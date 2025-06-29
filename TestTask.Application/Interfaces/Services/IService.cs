namespace TestTask.Application.Interfaces.Services
{
    public interface IService<T>
    {
        IEnumerable<T> GetAll();
        T? GetById(int id);
        void Add(T employee);
        void Update(T employee);
        void Delete(T employee);
    }
}
