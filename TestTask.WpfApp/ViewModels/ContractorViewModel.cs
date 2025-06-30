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
    public partial class ContractorViewModel : ObservableObject
    {
        private readonly IContractorService contractorService;
        private readonly IEmployeeService employeeService;
        private readonly IOrderService orderService;

        public ObservableCollection<Contractor> Contractors { get; } = new();

        [ObservableProperty]
        private Contractor? selectedContractor;

        public IAsyncRelayCommand AddCommand { get; }
        public IAsyncRelayCommand EditCommand { get; }
        public IAsyncRelayCommand DeleteCommand { get; }

        public ContractorViewModel(IContractorService contractorService, IEmployeeService employeeService, IOrderService orderService)
        {
            this.contractorService = contractorService;
            this.employeeService = employeeService;
            this.orderService = orderService;

            AddCommand = new AsyncRelayCommand(AddContractorAsync);
            EditCommand = new AsyncRelayCommand(EditContractorAsync, CanEditOrDelete);
            DeleteCommand = new AsyncRelayCommand(DeleteContractorAsync, CanEditOrDelete);

            _ = LoadContractorsAsync();
        }

        partial void OnSelectedContractorChanged(Contractor? value)
        {
            EditCommand.NotifyCanExecuteChanged();
            DeleteCommand.NotifyCanExecuteChanged();
        }

        private async Task LoadContractorsAsync()
        {
            Contractors.Clear();
            var contractors = await contractorService.GetAllAsync();
            foreach (var contractor in contractors)
                Contractors.Add(contractor);
        }

        private async Task AddContractorAsync()
        {
            var employees = await employeeService.GetAllAsync();
            var vm = new ContractorEditViewModel(employees);
            var window = new ContractorEditView { DataContext = vm };
            if (window.ShowDialog() == true)
            {
                await contractorService.AddAsync(vm.GetResult());
                await LoadContractorsAsync();
            }
        }

        private async Task EditContractorAsync()
        {
            if (SelectedContractor == null) return;
            var employees = await employeeService.GetAllAsync();
            var vm = new ContractorEditViewModel(SelectedContractor, employees);
            var window = new ContractorEditView { DataContext = vm };
            if (window.ShowDialog() == true)
            {
                await contractorService.UpdateAsync(vm.GetResult());
                await LoadContractorsAsync();
            }
        }

        private async Task DeleteContractorAsync()
        {
            if (SelectedContractor == null) return;
            // Проверяем, есть ли заказы с этим сотрудником
            var orders = await orderService.GetAllAsync();
            if (orders.Any(o => o.ContractorId == SelectedContractor.Id))
            {
                MessageBox.Show(
                    "Нельзя удалить контрагента, так как он используется в заказах.",
                    "Удаление запрещено",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }
            await contractorService.DeleteAsync(SelectedContractor.Id);
            await LoadContractorsAsync();
        }

        private bool CanEditOrDelete()
        {
            return SelectedContractor != null;
        }
    }
}