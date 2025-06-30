using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TeskTask.Core.Models;
using TestTask.Application.Interfaces.Services;

namespace TestTask.WPF.ViewModels
{
    public class EmployeeViewModel : BaseViewModel
    {
        private readonly IEmployeeService _employeeService;

        public ObservableCollection<Employee> Employees { get; set; } = new();
        public Employee? SelectedEmployee { get; set; }

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public EmployeeViewModel(IEmployeeService employeeService)
        {
            _employeeService = employeeService;

            AddCommand = new RelayCommand(async _ => await AddEmployee());
            EditCommand = new RelayCommand(async _ => await EditEmployee(), _ => SelectedEmployee != null);
            DeleteCommand = new RelayCommand(async _ => await DeleteEmployee(), _ => SelectedEmployee != null);

            _ = LoadEmployees();
        }

        private async Task LoadEmployees()
        {
            Employees.Clear();
            var employees = await _employeeService.GetAllAsync();
            foreach (var employee in employees)
                Employees.Add(employee);
        }

        private async Task AddEmployee()
        {
            var vm = new EmployeeEditViewModel();
            var window = new EmployeeEditView { DataContext = vm };
            if (window.ShowDialog() == true)
            {
                await _employeeService.AddAsync(vm.Employee);
                await LoadEmployees();
            }
        }

        private async Task EditEmployee()
        {
            if (SelectedEmployee == null) return;
            var vm = new EmployeeEditViewModel(SelectedEmployee);
            var window = new EmployeeEditView { DataContext = vm };
            if (window.ShowDialog() == true)
            {
                await _employeeService.UpdateAsync(vm.Employee);
                await LoadEmployees();
            }
        }

        private async Task DeleteEmployee()
        {
            if (SelectedEmployee == null) return;
            await _employeeService.DeleteAsync(SelectedEmployee.Id);
            await LoadEmployees();
        }
    }
}