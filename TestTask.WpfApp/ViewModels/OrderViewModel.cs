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
        private readonly IOrderService _orderService;
        private readonly IEmployeeService _employeeService;
        private readonly IContractorService _contractorService;

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
            _orderService = orderService;
            _employeeService = employeeService;
            _contractorService = contractorService;

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
            var orders = await _orderService.GetAllAsync();
            foreach (var order in orders)
                Orders.Add(order);
        }

        private async Task AddOrderAsync()
        {
            var employees = await _employeeService.GetAllAsync();
            var contractors = await _contractorService.GetAllAsync();
            var vm = new OrderEditViewModel(employees, contractors);
            var window = new OrderEditView { DataContext = vm };
            if (window.ShowDialog() == true)
            {
                await _orderService.AddAsync(vm.GetResult());
                await LoadOrdersAsync();
            }
        }

        private async Task EditOrderAsync()
        {
            if (SelectedOrder == null) return;
            var employees = await _employeeService.GetAllAsync();
            var contractors = await _contractorService.GetAllAsync();
            var vm = new OrderEditViewModel(SelectedOrder, employees, contractors);
            var window = new OrderEditView { DataContext = vm };
            if (window.ShowDialog() == true)
            {
                await _orderService.UpdateAsync(vm.GetResult());
                await LoadOrdersAsync();
            }
        }

        private async Task DeleteOrderAsync()
        {
            if (SelectedOrder == null) return;
            await _orderService.DeleteAsync(SelectedOrder.Id);
            await LoadOrdersAsync();
        }

        private bool CanEditOrDelete()
        {
            return SelectedOrder != null;
        }
    }
}