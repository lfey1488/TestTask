namespace TestTask.Application.Interfaces.Services
{
    public interface IService<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T employee);
        Task UpdateAsync(T employee);
        Task DeleteAsync(T employee);
    }
}
