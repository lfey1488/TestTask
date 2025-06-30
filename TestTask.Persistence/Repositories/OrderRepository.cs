using AutoMapper;
using NHibernate;
using TeskTask.Core.Models;
using TestTask.Application.Interfaces.Repositories;
using TestTask.Persistence.Entities;

namespace TestTask.Persistence.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private readonly ISessionFactory sessionFactory;
        private readonly IMapper mapper;

        public OrderRepository(ISessionFactory sessionFactory, IMapper mapper)
        {
            this.sessionFactory = sessionFactory;
            this.mapper = mapper;
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            using var session = sessionFactory.OpenSession();
            var orderEntity = await session.GetAsync<OrderEntity>(id);

            return mapper.Map<Order>(orderEntity);
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            using var session = sessionFactory.OpenSession();
            var orderEntities = await session.QueryOver<OrderEntity>().ListAsync();

            return mapper.Map<IEnumerable<Order>>(orderEntities);
        }

        public async Task AddAsync(Order model)
        {
            using var session = sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();
            try
            {
                var entity = mapper.Map<OrderEntity>(model);
                // По-хорошему настроить автоматический маппинг
                entity.Contractor = await session.GetAsync<ContractorEntity>(model.ContractorId);
                entity.Employee = await session.GetAsync<EmployeeEntity>(model.EmployeeId);
                await session.SaveAsync(entity);
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task UpdateAsync(Order model)
        {
            using var session = sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();
            try
            {
                var entity = mapper.Map<OrderEntity>(model);
                await session.UpdateAsync(entity);
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteAsync(Order model)
        {
            using var session = sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();
            try
            {
                var entity = mapper.Map<OrderEntity>(model);
                await session.DeleteAsync(entity);
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}