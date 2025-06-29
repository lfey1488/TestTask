using NHibernate;
using TeskTask.Core.Models;
using TestTask.Application.Interfaces.Repositories;

namespace TestTask.Persistence.Repositories
{
    public class ContractorRepository : IRepository<Contractor>
    {
        private readonly ISessionFactory sessionFactory;

        public ContractorRepository(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }

        public async Task<Contractor?> GetByIdAsync(int id)
        {
            return await Task.Run(() =>
            {
                using var session = sessionFactory.OpenSession();
                return session.Get<Contractor>(id);
            });
        }

        public async Task<IEnumerable<Contractor>> GetAllAsync()
        {
            return await Task.Run(() =>
            {
                using var session = sessionFactory.OpenSession();
                return session.QueryOver<Contractor>().List();
            });
        }

        public async Task AddAsync(Contractor entity)
        {
            await Task.Run(() =>
            {
                using var session = sessionFactory.OpenSession();
                using var transaction = session.BeginTransaction();
                session.Save(entity);
                transaction.Commit();
            });
        }

        public async Task UpdateAsync(Contractor entity)
        {
            await Task.Run(() =>
            {
                using var session = sessionFactory.OpenSession();
                using var transaction = session.BeginTransaction();
                session.Update(entity);
                transaction.Commit();
            });
        }

        public async Task DeleteAsync(Contractor entity)
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