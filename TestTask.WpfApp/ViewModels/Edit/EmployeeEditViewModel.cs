using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TeskTask.Core.Enums;
using TeskTask.Core.Models;

namespace TestTask.WpfApp.ViewModels.Edit
{
    public class EmployeeEditViewModel : INotifyPropertyChanged
    {
        private string _fullName;
        private Position _position;
        private DateTime _birthDate;

        public string FullName
        {
            get => _fullName;
            set { _fullName = value; OnPropertyChanged(); }
        }

        public Position Position
        {
            get => _position;
            set { _position = value; OnPropertyChanged(); }
        }

        public DateTime BirthDate
        {
            get => _birthDate;
            set { _birthDate = value; OnPropertyChanged(); }
        }

        public EmployeeEditViewModel()
        {
            FullName = string.Empty;
            Position = Position.Worker;
            BirthDate = DateTime.Today;
        }

        public EmployeeEditViewModel(Employee employee)
        {
            FullName = employee.FullName;
            Position = employee.Position;
            BirthDate = employee.BirthDate;
        }

        public Employee GetResult()
        {
            return Employee.Create(FullName, Position, BirthDate);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}