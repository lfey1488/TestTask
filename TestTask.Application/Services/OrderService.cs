using TeskTask.Core.Models;
using TestTask.Application.Interfaces.Repositories;
using TestTask.Application.Interfaces.Services;

namespace TestTask.Application.Services
{
    public class OrderService : IService<Order>, IOrderService
    {
        private readonly IRepository<Order> orderRepository;
        private readonly IRepository<Employee> employeeRepository;
        private readonly IRepository<Contractor> contractorRepository;
        public OrderService(IRepository<Order> orderRepository,
            IRepository<Employee> employeeRepository,
            IRepository<Contractor> contractorRepository)
        {
            this.orderRepository = orderRepository;
            this.employeeRepository = employeeRepository;
            this.contractorRepository = contractorRepository;
        }
        public async Task AddAsync(Order order)
        {
            if (order is null)
                throw new ArgumentNullException(nameof(order), "Order cannot be null");
            await orderRepository.AddAsync(order);
        }

        public async Task ChangeAmount(int orderId, decimal amount)
        {
            if (amount < 0)
                throw new ArgumentException("The amount cannot be negative.", nameof(amount));
            var order = await orderRepository.GetByIdAsync(orderId)
                ?? throw new Exception("Order not found");

            order.ChangeAmount(amount);
            await orderRepository.UpdateAsync(order);
        }

        public async Task ChangeContractor(int orderId, int contractorId)
        {
            if (orderId <= 0)
                throw new ArgumentException("Order ID must be a positive number.", nameof(orderId));
            if (contractorId <= 0)
                throw new ArgumentException("Contractor ID must be a positive number.", nameof(contractorId));
            var order = await orderRepository.GetByIdAsync(orderId)
                ?? throw new Exception("Order not found");
            var contractor = await contractorRepository.GetByIdAsync(contractorId)
                ?? throw new Exception("Contractor not found");

            order.ChangeContractor(contractor);
            await orderRepository.UpdateAsync(order);
        }

        public async Task ChangeDate(int orderId, DateTime date)
        {
            if (date > DateTime.Now)
                throw new ArgumentException("The date cannot be in the future.", nameof(date));
            if (orderId <= 0)
                throw new ArgumentException("Order ID must be a positive number.", nameof(orderId));

            var order = await orderRepository.GetByIdAsync(orderId)
                ?? throw new Exception("Order not found");

            order.ChangeDate(date);
            await orderRepository.UpdateAsync(order);
        }

        public async Task ChangeEmployee(int orderId, int employeeId)
        {
            if (orderId <= 0)
                throw new ArgumentException("Order ID must be a positive number.", nameof(orderId));
            if (employeeId <= 0)
                throw new ArgumentException("Employee ID must be a positive number.", nameof(employeeId));
            var order = await orderRepository.GetByIdAsync(orderId)
                ?? throw new Exception("Order not found");
            var employee = await employeeRepository.GetByIdAsync(employeeId)
                ?? throw new Exception("Employee not found");

            order.ChangeEmployee(employee);
            await orderRepository.UpdateAsync(order);
        }

        public async Task DeleteAsync(Order order)
        {
            if (order is null)
                throw new ArgumentNullException(nameof(order), "Order cannot be null");
            await orderRepository.DeleteAsync(order);
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await orderRepository.GetAllAsync();
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID must be a positive number.", nameof(id));
            return await orderRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Order order)
        {
            if (order is null)
                throw new ArgumentNullException(nameof(order), "Order cannot be null");
            await orderRepository.UpdateAsync(order);
        }
    }
}
