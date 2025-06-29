namespace TestTask.Application.Interfaces.Services
{
    public interface IOrderService
    {
        Task ChangeDate(int orderId, DateTime date);
        Task ChangeAmount(int orderId, decimal amount);
        Task ChangeEmployee(int orderId, int employeeId);
        Task ChangeContractor(int orderId, int contractorId);
    }
}
