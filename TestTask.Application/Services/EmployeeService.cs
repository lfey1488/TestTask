using TeskTask.Core.Models;
using TestTask.Application.Interfaces.Repositories;
using TestTask.Application.Interfaces.Services;

namespace TestTask.Application.Services
{
    public class EmployeeService : IService<Employee>
    {
        private readonly IRepository<Employee> employeeRepository;
        public EmployeeService(IRepository<Employee> employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }
        public Task AddAsync(Employee employee)
        {
            return employeeRepository.AddAsync(employee);
        }

        public Task DeleteAsync(Employee employee)
        {
            return employeeRepository.DeleteAsync(employee);
        }

        public Task<IEnumerable<Employee>> GetAllAsync()
        {
            return employeeRepository.GetAllAsync();
        }

        public Task<Employee?> GetByIdAsync(int id)
        {
            return employeeRepository.GetByIdAsync(id);
        }

        public Task UpdateAsync(Employee employee)
        {
            return employeeRepository.UpdateAsync(employee);
        }
    }
}
