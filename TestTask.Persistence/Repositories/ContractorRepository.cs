using AutoMapper;
using NHibernate;
using System.Reflection;
using TeskTask.Core.Models;
using TestTask.Application.Interfaces.Repositories;
using TestTask.Persistence.Entities;

namespace TestTask.Persistence.Repositories
{
    public class ContractorRepository : IRepository<Contractor>
    {
        private readonly ISessionFactory sessionFactory;
        private readonly IMapper mapper;

        public ContractorRepository(ISessionFactory sessionFactory, IMapper mapper)
        {
            this.sessionFactory = sessionFactory;
            this.mapper = mapper;
        }

        public async Task<Contractor?> GetByIdAsync(int id)
        {
            using var session = sessionFactory.OpenSession();
            var contractor = await session.GetAsync<ContractorEntity>(id);

            return mapper.Map<Contractor>(contractor);
        }

        public async Task<IEnumerable<Contractor>> GetAllAsync()
        {
            using var session = sessionFactory.OpenSession();
            var contractors = await session
                .QueryOver<ContractorEntity>()
                .ListAsync();

            return mapper.Map<IEnumerable<Contractor>>(contractors);
        }

        public async Task AddAsync(Contractor model)
        {
            using var session = sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();
            try
            {
                var entity = mapper.Map<ContractorEntity>(model);
                await session.SaveAsync(entity);
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task UpdateAsync(Contractor model)
        {
            using var session = sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();
            try
            {
                var entity = mapper.Map<ContractorEntity>(model);
                await session.UpdateAsync(entity);
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteAsync(Contractor model)
        {
            using var session = sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();
            try
            {
                var entity = mapper.Map<ContractorEntity>(model);
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