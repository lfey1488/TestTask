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
            return await Task.Run(() =>
            {
                using var session = sessionFactory.OpenSession();
                return session.Get<Order>(id);
            });
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await Task.Run(() =>
            {
                using var session = sessionFactory.OpenSession();
                return session.QueryOver<Order>().List();
            });
        }

        public async Task AddAsync(Order entity)
        {
            await Task.Run(() =>
            {
                using var session = sessionFactory.OpenSession();
                using var transaction = session.BeginTransaction();
                session.Save(entity);
                transaction.Commit();
            });
        }

        public async Task UpdateAsync(Order entity)
        {
            await Task.Run(() =>
            {
                using var session = sessionFactory.OpenSession();
                using var transaction = session.BeginTransaction();
                session.Update(entity);
                transaction.Commit();
            });
        }

        public async Task DeleteAsync(Order entity)
        {
            await Task.Run(() =>
            {
                using var session = sessionFactory.OpenSession();
                using var transaction = session.BeginTransaction();
                session.Delete(entity);
                transaction.Commit();
            });
        }
    }
}