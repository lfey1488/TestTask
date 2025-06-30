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
    public partial class OrderViewModel : ObservableObject
    {
        private readonly IOrderService orderService;
        private readonly IEmployeeService employeeService;
        private readonly IContractorService contractorService;

        public ObservableCollection<Order> Orders { get; } = new();

        [ObservableProperty]
        private Order? selectedOrder;

        public IAsyncRelayCommand AddCommand { get; }
        public IAsyncRelayCommand EditCommand { get; }
        public IAsyncRelayCommand DeleteCommand { get; }

        public OrderViewModel(
            IOrderService orderService,
            IEmployeeService employeeService,
            IContractorService contractorService)
        {
            this.orderService = orderService;
            this.employeeService = employeeService;
            this.contractorService = contractorService;

            AddCommand = new AsyncRelayCommand(AddOrderAsync);
            EditCommand = new AsyncRelayCommand(EditOrderAsync, CanEditOrDelete);
            DeleteCommand = new AsyncRelayCommand(DeleteOrderAsync, CanEditOrDelete);

            _ = LoadOrdersAsync();
        }

        partial void OnSelectedOrderChanged(Order? value)
        {
            EditCommand.NotifyCanExecuteChanged();
            DeleteCommand.NotifyCanExecuteChanged();
        }

        private async Task LoadOrdersAsync()
        {
            Orders.Clear();
            var orders = await orderService.GetAllAsync();
            foreach (var order in orders)
                Orders.Add(order);
        }

        private async Task AddOrderAsync()
        {
            var employees = await employeeService.GetAllAsync();
            var contractors = await contractorService.GetAllAsync();
            var vm = new OrderEditViewModel(employees, contractors);
            var window = new OrderEditView { DataContext = vm };
            if (window.ShowDialog() == true)
            {
                await orderService.AddAsync(vm.GetResult());
                await LoadOrdersAsync();
            }
        }

        private async Task EditOrderAsync()
        {
            if (SelectedOrder == null) return;
            var employees = await employeeService.GetAllAsync();
            var contractors = await contractorService.GetAllAsync();
            var vm = new OrderEditViewModel(SelectedOrder, employees, contractors);
            var window = new OrderEditView { DataContext = vm };
            if (window.ShowDialog() == true)
            {
                await orderService.UpdateAsync(vm.GetResult());
                await LoadOrdersAsync();
            }
        }

        private async Task DeleteOrderAsync()
        {
            if (SelectedOrder == null) return;
            await orderService.DeleteAsync(SelectedOrder.Id);
            await LoadOrdersAsync();
        }

        private bool CanEditOrDelete()
        {
            return SelectedOrder != null;
        }
    }
}