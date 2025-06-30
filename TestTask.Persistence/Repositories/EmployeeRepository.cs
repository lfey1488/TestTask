using AutoMapper;
using NHibernate;
using TeskTask.Core.Models;
using TestTask.Application.Interfaces.Repositories;
using TestTask.Persistence.Entities;

namespace TestTask.Persistence.Repositories
{
    public class EmployeeRepository : IRepository<Employee>
    {
        private readonly ISessionFactory sessionFactory;
        private readonly IMapper mapper;

        public EmployeeRepository(ISessionFactory sessionFactory, IMapper mapper)
        {
            this.sessionFactory = sessionFactory;
            this.mapper = mapper;
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            using var session = sessionFactory.OpenSession();
            var employeeEntity = await session.GetAsync<EmployeeEntity>(id);
            return mapper.Map<Employee>(employeeEntity);
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            using var session = sessionFactory.OpenSession();
            var employeeEntities = await session
                .QueryOver<EmployeeEntity>()
                .ListAsync();

            return mapper.Map<IEnumerable<Employee>>(employeeEntities);
        }

        public async Task AddAsync(Employee model)
        {
            using var session = sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();
            try
            {
                var entity = mapper.Map<EmployeeEntity>(model);
                await session.SaveAsync(entity);
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task UpdateAsync(Employee model)
        {
            using var session = sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();
            try
            {
                var entity = mapper.Map<EmployeeEntity>(model);
                await session.UpdateAsync(entity);
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteAsync(Employee model)
        {
            using var session = sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();
            try
            {
                var entity = mapper.Map<EmployeeEntity>(model);
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