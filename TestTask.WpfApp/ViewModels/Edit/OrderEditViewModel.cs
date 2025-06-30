using System.ComponentModel;
using System.Runtime.CompilerServices;
using TeskTask.Core.Models;

namespace TestTask.WpfApp.ViewModels.Edit
{
    public class OrderEditViewModel : INotifyPropertyChanged
    {
        private DateTime _date;
        private decimal _amount;
        private int _employeeId;
        private int _contractorId;

        public IEnumerable<Employee> Employees { get; }
        public IEnumerable<Contractor> Contractors { get; }

        public DateTime Date
        {
            get => _date;
            set { _date = value; OnPropertyChanged(); }
        }

        public decimal Amount
        {
            get => _amount;
            set { _amount = value; OnPropertyChanged(); }
        }

        public int EmployeeId
        {
            get => _employeeId;
            set { _employeeId = value; OnPropertyChanged(); }
        }

        public int ContractorId
        {
            get => _contractorId;
            set { _contractorId = value; OnPropertyChanged(); }
        }

        public OrderEditViewModel(IEnumerable<Employee> employees, IEnumerable<Contractor> contractors)
        {
            Employees = employees;
            Contractors = contractors;
            Date = DateTime.Today;
            Amount = 0;
            EmployeeId = employees.FirstOrDefault()?.Id ?? 0;
            ContractorId = contractors.FirstOrDefault()?.Id ?? 0;
        }

        public OrderEditViewModel(Order order, IEnumerable<Employee> employees, IEnumerable<Contractor> contractors)
        {
            Employees = employees;
            Contractors = contractors;
            Date = order.Date;
            Amount = order.Amount;
            EmployeeId = order.EmployeeId;
            ContractorId = order.ContractorId;
        }

        public Order GetResult()
        {
            return Order.Create(Date, Amount, EmployeeId, ContractorId);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}