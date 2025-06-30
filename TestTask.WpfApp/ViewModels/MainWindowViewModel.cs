
namespace TestTask.WpfApp.ViewModels
{
    public class MainWindowViewModel
    {
        public EmployeeViewModel EmployeeViewModel { get; }
        public ContractorViewModel ContractorViewModel { get; }
        public OrderViewModel OrderViewModel { get; }

        public MainWindowViewModel(
            EmployeeViewModel employeeViewModel,
            ContractorViewModel contractorViewModel,
            OrderViewModel orderViewModel)
        {
            EmployeeViewModel = employeeViewModel;
            ContractorViewModel = contractorViewModel;
            OrderViewModel = orderViewModel;
        }
    }
}