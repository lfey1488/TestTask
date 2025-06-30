using AutoMapper;
using TeskTask.Core.Enums;
using TeskTask.Core.Models;
using TestTask.Persistence;
using TestTask.Persistence.Repositories;
using Xunit;

namespace TestTask.Tests.Persistence.Repositories
{
    public class EmployeeRepositoryTests
    {
        private readonly EmployeeRepository _repository;

        public EmployeeRepositoryTests()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
                cfg.AddProfile<TestTask.Persistence.Configurations.PersistenceProfile>());
            var mapper = mapperConfig.CreateMapper();
            _repository = new EmployeeRepository(NHibernateHelper.SessionFactory, mapper);
        }

        [Fact]
        public async Task AddAsync_ShouldAddAndGetById()
        {
            var employee = new Employee("Ivanov Ivan", Position.Manager, new DateTime(1990, 1, 1));
            await _repository.AddAsync(employee);

            var fromDb = await _repository.GetAllAsync();
            Assert.Contains(fromDb, e => e.FullName == "Ivanov Ivan");
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateEntity()
        {
            var employee = new Employee("ToUpdate", Position.Worker, new DateTime(1995, 5, 5));
            await _repository.AddAsync(employee);

            var added = (await _repository.GetAllAsync()).First(e => e.FullName == "ToUpdate");
            added.ChangeFullName("Updated Name");
            await _repository.UpdateAsync(added);

            var updated = await _repository.GetByIdAsync(added.Id);
            Assert.Equal("Updated Name", updated?.FullName);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveEntity()
        {
            var employee = new Employee("ToDelete", Position.Worker, new DateTime(1995, 5, 5));
            await _repository.AddAsync(employee);

            var added = (await _repository.GetAllAsync()).First(e => e.FullName == "ToDelete");
            await _repository.DeleteAsync(added);

            var deleted = await _repository.GetByIdAsync(added.Id);
            Assert.Null(deleted);
        }

        [Fact]
        public async Task AddAsync_ShouldRollbackOnException()
        {
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                using var session = NHibernateHelper.SessionFactory.OpenSession();
                using var transaction = session.BeginTransaction();
                try
                {
                    // Некорректные данные (например, FullName = null)
                    var entity = new TestTask.Persistence.Entities.EmployeeEntity
                    {
                        FullName = null!,
                        Position = (int)Position.Manager,
                        BirthDate = DateTime.Now
                    };
                    await session.SaveAsync(entity);
                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            });
        }
    }
}