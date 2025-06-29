using NHibernate;
using TeskTask.Core.Models;
using TestTask.Application.Interfaces.Repositories;

namespace TestTask.Persistence.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private readonly ISessionFactory sessionFactory;

        public OrderRepository(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            using var session = sessionFactory.OpenSession();
            return await session.GetAsync<Order>(id);
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            using var session = sessionFactory.OpenSession();
            return await session.QueryOver<Order>().ListAsync();
        }

        public async Task AddAsync(Order entity)
        {
            using var session = sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();
            try
            {
                await session.SaveAsync(entity);
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task UpdateAsync(Order entity)
        {
            using var session = sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();
            try
            {
                await session.UpdateAsync(entity);
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteAsync(Order entity)
        {
            using var session = sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();
            try
            {
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