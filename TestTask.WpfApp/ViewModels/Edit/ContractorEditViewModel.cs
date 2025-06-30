using System.ComponentModel;
using System.Runtime.CompilerServices;
using TeskTask.Core.Models;

namespace TestTask.WpfApp.ViewModels.Edit
{
    public class ContractorEditViewModel : INotifyPropertyChanged
    {
        private string _name = string.Empty;
        private int _inn;
        private int _curatorId;

        public IEnumerable<Employee> Employees { get; }

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        public int Inn
        {
            get => _inn;
            set { _inn = value; OnPropertyChanged(); }
        }

        public int CuratorId
        {
            get => _curatorId;
            set { _curatorId = value; OnPropertyChanged(); }
        }

        public ContractorEditViewModel(IEnumerable<Employee> employees)
        {
            Employees = employees;
            Name = "";
            Inn = 0;
            CuratorId = employees.FirstOrDefault()?.Id ?? 0;
        }

        public ContractorEditViewModel(Contractor contractor, IEnumerable<Employee> employees)
        {
            Employees = employees;
            Name = contractor.Name;
            Inn = contractor.Inn;
            CuratorId = contractor.CuratorId;
        }

        public Contractor GetResult()
        {
            return Contractor.Create(Name, Inn, CuratorId);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}