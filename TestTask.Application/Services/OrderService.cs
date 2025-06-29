using TeskTask.Core.Models;
using TestTask.Application.Interfaces.Repositories;
using TestTask.Application.Interfaces.Services;

namespace TestTask.Application.Services
{
    public class OrderService : IService<Order>
    {
        private readonly IRepository<Order> orderRepository;
        public OrderService(IRepository<Order> orderRepository)
        {
            this.orderRepository = orderRepository;
        }
        public Task AddAsync(Order employee)
        {
            return orderRepository.AddAsync(employee);
        }

        public Task DeleteAsync(Order employee)
        {
            return orderRepository.DeleteAsync(employee);
        }

        public Task<IEnumerable<Order>> GetAllAsync()
        {
            return orderRepository.GetAllAsync();
        }

        public Task<Order?> GetByIdAsync(int id)
        {
            return orderRepository.GetByIdAsync(id);
        }

        public Task UpdateAsync(Order employee)
        {
            return orderRepository.UpdateAsync(employee);
        }
    }
}
