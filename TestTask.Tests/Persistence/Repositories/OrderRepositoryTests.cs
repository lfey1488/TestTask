using AutoMapper;
using TeskTask.Core.Models;
using TestTask.Persistence;
using TestTask.Persistence.Entities;
using TestTask.Persistence.Repositories;

namespace TestTask.Tests.Persistence.Repositories
{
    public class OrderRepositoryTests
    {
        private readonly OrderRepository _repository;

        public OrderRepositoryTests()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
                cfg.AddProfile<TestTask.Persistence.Configurations.PersistenceProfile>());
            var mapper = mapperConfig.CreateMapper();
            _repository = new OrderRepository(NHibernateHelper.SessionFactory, mapper);
        }

        [Fact]
        public async Task AddAsync_ShouldAddAndGetById()
        {
            var order = new Order(DateTime.Today, 1000, 1, 1);
            await _repository.AddAsync(order);

            var fromDb = await _repository.GetAllAsync();
            Assert.Contains(fromDb, o => o.Amount == 1000);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateEntity()
        {
            var order = new Order(DateTime.Today, 2000, 1, 1);
            await _repository.AddAsync(order);

            var added = (await _repository.GetAllAsync()).First(o => o.Amount == 2000);
            added.ChangeAmount(3000);
            await _repository.UpdateAsync(added);

            var updated = await _repository.GetByIdAsync(added.Id);
            Assert.Equal(3000, updated?.Amount);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveEntity()
        {
            var order = new Order(DateTime.Today, 4000, 1, 1);
            await _repository.AddAsync(order);

            var added = (await _repository.GetAllAsync()).First(o => o.Amount == 4000);
            await _repository.DeleteAsync(added);

            var deleted = await _repository.GetByIdAsync(added.Id);
            Assert.Null(deleted);
        }

        [Fact]
        public async Task AddAsync_ShouldRollbackOnException()
        {
        }
    }
}