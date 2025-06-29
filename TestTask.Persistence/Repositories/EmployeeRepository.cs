using NHibernate;
using TeskTask.Core.Models;
using TestTask.Application.Interfaces.Repositories;

namespace TestTask.Persistence.Repositories
{
    public class EmployeeRepository : IRepository<Employee>
    {
        private readonly ISessionFactory sessionFactory;

        public EmployeeRepository(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await Task.Run(() =>
            {
                using var session = sessionFactory.OpenSession();
                return session.Get<Employee>(id);
            });
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await Task.Run(() =>
            {
                using var session = sessionFactory.OpenSession();
                return session.QueryOver<Employee>().List();
            });
        }

        public async Task AddAsync(Employee entity)
        {
            await Task.Run(() =>
            {
                using var session = sessionFactory.OpenSession();
                using var transaction = session.BeginTransaction();
                session.Save(entity);
                transaction.Commit();
            });
        }

        public async Task UpdateAsync(Employee entity)
        {
            await Task.Run(() =>
            {
                using var session = sessionFactory.OpenSession();
                using var transaction = session.BeginTransaction();
                session.Update(entity);
                transaction.Commit();
            });
        }

        public async Task DeleteAsync(Employee entity)
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