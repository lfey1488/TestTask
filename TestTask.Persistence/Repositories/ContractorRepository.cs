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
            using var session = sessionFactory.OpenSession();
            return await session.GetAsync<Contractor>(id);
        }

        public async Task<IEnumerable<Contractor>> GetAllAsync()
        {
            using var session = sessionFactory.OpenSession();
            return await session.QueryOver<Contractor>().ListAsync();
        }

        public async Task AddAsync(Contractor entity)
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

        public async Task UpdateAsync(Contractor entity)
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

        public async Task DeleteAsync(Contractor entity)
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