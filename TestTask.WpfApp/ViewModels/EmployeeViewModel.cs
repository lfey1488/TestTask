using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TeskTask.Core.Models;
using TestTask.Application.Interfaces.Services;
using TestTask.WpfApp.ViewModels.Edit;
using TestTask.WpfApp.Views.Edit;

namespace TestTask.WpfApp.ViewModels
{
    public partial class EmployeeViewModel : ObservableObject
    {
        private readonly IEmployeeService _employeeService;

        public ObservableCollection<Employee> Employees { get; } = new();

        [ObservableProperty]
        private Employee? selectedEmployee;

        public IAsyncRelayCommand AddCommand { get; }
        public IAsyncRelayCommand EditCommand { get; }
        public IAsyncRelayCommand DeleteCommand { get; }

        public EmployeeViewModel(IEmployeeService employeeService)
        {
            _employeeService = employeeService;

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
            var employees = await _employeeService.GetAllAsync();
            foreach (var employee in employees)
                Employees.Add(employee);
        }

        private async Task AddEmployeeAsync()
        {
            var vm = new EmployeeEditViewModel();
            var window = new EmployeeEditView { DataContext = vm };
            if (window.ShowDialog() == true)
            {
                await _employeeService.AddAsync(vm.GetResult());
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
                await _employeeService.UpdateAsync(vm.GetResult());
                await LoadEmployeesAsync();
            }
        }

        private async Task DeleteEmployeeAsync()
        {
            if (SelectedEmployee == null) return;
            await _employeeService.DeleteAsync(SelectedEmployee.Id);
            await LoadEmployeesAsync();
        }

        private bool CanEditOrDelete()
        {
            return SelectedEmployee != null;
        }
    }
}