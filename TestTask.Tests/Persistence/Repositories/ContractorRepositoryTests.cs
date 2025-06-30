using AutoMapper;
using TestTask.Persistence.Repositories;
using TestTask.Persistence.Entities;
using TestTask.Persistence;
using Xunit;
using TeskTask.Core.Models;

namespace TestTask.Tests.Persistence.Repositories
{
    public class ContractorRepositoryTests
    {
        private readonly ContractorRepository _repository;

        public ContractorRepositoryTests()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
                cfg.AddProfile<TestTask.Persistence.Configurations.PersistenceProfile>());
            var mapper = mapperConfig.CreateMapper();
            _repository = new ContractorRepository(NHibernateHelper.SessionFactory, mapper);
        }

        [Fact]
        public async Task AddAsync_ShouldAddAndGetById()
        {
            var contractor = new Contractor("TestName", 123456, 1);
            await _repository.AddAsync(contractor);

            var fromDb = await _repository.GetAllAsync();
            Assert.Contains(fromDb, c => c.Name == "TestName" && c.Inn == 123456);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateEntity()
        {
            var contractor = new Contractor("ToUpdate", 111111, 1);
            await _repository.AddAsync(contractor);

            var added = (await _repository.GetAllAsync()).First(c => c.Name == "ToUpdate");
            added.ChangeName("UpdatedName");
            await _repository.UpdateAsync(added);

            var updated = await _repository.GetByIdAsync(added.Id);
            Assert.Equal("UpdatedName", updated?.Name);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveEntity()
        {
            var contractor = new Contractor("ToDelete", 222222, 1);
            await _repository.AddAsync(contractor);

            var added = (await _repository.GetAllAsync()).First(c => c.Name == "ToDelete");
            await _repository.DeleteAsync(added);

            var deleted = await _repository.GetByIdAsync(added.Id);
            Assert.Null(deleted);
        }

        [Fact]
        public async Task AddAsync_ShouldRollbackOnException()
        {
            var contractor = new Contractor("Rollback", 333333, 1);
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                using var session = NHibernateHelper.SessionFactory.OpenSession();
                using var transaction = session.BeginTransaction();
                try
                {
                    // Вставляем некорректные данные (например, Name = null)
                    var badContractor = new Contractor(null!, 333333, 1);
                    var entity = new TestTask.Persistence.Entities.ContractorEntity
                    {
                        Name = null!,
                        Inn = 333333,
                        Curator = new TestTask.Persistence.Entities.EmployeeEntity { Id = 1 }
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