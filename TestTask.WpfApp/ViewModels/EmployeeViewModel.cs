using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows;
using TeskTask.Core.Models;
using TestTask.Application.Interfaces.Services;
using TestTask.WpfApp.ViewModels.Edit;
using TestTask.WpfApp.Views.Edit;

namespace TestTask.WpfApp.ViewModels
{
    public partial class EmployeeViewModel : ObservableObject
    {
        private readonly IEmployeeService employeeService;
        private readonly IOrderService orderService;
        private readonly IContractorService contractorService;

        public ObservableCollection<Employee> Employees { get; } = new();

        [ObservableProperty]
        private Employee? selectedEmployee;

        public IAsyncRelayCommand AddCommand { get; }
        public IAsyncRelayCommand EditCommand { get; }
        public IAsyncRelayCommand DeleteCommand { get; }

        public EmployeeViewModel(IEmployeeService employeeService, IOrderService orderService, IContractorService contractorService)
        {
            this.employeeService = employeeService;
            this.orderService = orderService;
            this.contractorService = contractorService;

            AddCommand = new AsyncRelayCommand(AddEmployeeAsync);
            EditCommand = new AsyncRelayCommand(EditEmployeeAsync, CanEditOrDelete);
            DeleteCommand = new AsyncRelayCommand(DeleteEmployeeAsync, CanEditOrDelete);

            _ = LoadEmployeesAsync();
        }

        partial void OnSelectedEmployeeChanged(Employee? value)
        {
            EditCommand.NotifyCanExecuteChanged();
            DeleteCommand.NotifyCanExecuteChanged();
        }

        private async Task LoadEmployeesAsync()
        {
            Employees.Clear();
            var employees = await employeeService.GetAllAsync();
            foreach (var employee in employees)
                Employees.Add(employee);
        }

        private async Task AddEmployeeAsync()
        {
            var vm = new EmployeeEditViewModel();
            var window = new EmployeeEditView { DataContext = vm };
            if (window.ShowDialog() == true)
            {
                await employeeService.AddAsync(vm.GetResult());
                await LoadEmployeesAsync();
            }
        }

        private async Task EditEmployeeAsync()
        {
            if (SelectedEmployee == null) return;
            var vm = new EmployeeEditViewModel(SelectedEmployee);
            var window = new EmployeeEditView { DataContext = vm };
            if (window.ShowDialog() == true)
            {
                await employeeService.UpdateAsync(vm.GetResult());
                await LoadEmployeesAsync();
            }
        }

        private async Task DeleteEmployeeAsync()
        {
            if (SelectedEmployee == null) return;

            // Проверяем, есть ли заказы с этим сотрудником
            var orders = await orderService.GetAllAsync();
            if (orders.Any(o => o.EmployeeId == SelectedEmployee.Id))
            {
                MessageBox.Show(
                    "Нельзя удалить сотрудника, так как он используется в заказах.",
                    "Удаление запрещено",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }
            // Проверяем, есть ли контрагенты с этим сотрудником
            var contractors = await contractorService.GetAllAsync();
            if (contractors.Any(c => c.Id == SelectedEmployee.Id))
            {
                MessageBox.Show(
                    "Нельзя удалить сотрудника, так как он используется в контрагентах.",
                    "Удаление запрещено",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            await employeeService.DeleteAsync(SelectedEmployee.Id);
            await LoadEmployeesAsync();
        }

        private bool CanEditOrDelete()
        {
            return SelectedEmployee != null;
        }
    }
}