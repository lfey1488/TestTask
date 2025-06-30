using TeskTask.Core.Enums;
using TeskTask.Core.Models;
using TestTask.Application.Interfaces.Repositories;
using TestTask.Application.Interfaces.Services;

namespace TestTask.Application.Services
{
    public class EmployeeService : IService<Employee>, IEmployeeService
    {
        private readonly IRepository<Employee> employeeRepository;
        public EmployeeService(IRepository<Employee> employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }
        public async Task AddAsync(Employee employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(employee), "Employee cannot be null");

            await employeeRepository.AddAsync(employee);
        }

        public async Task ChangeBirthDate(int employeeId, DateTime newBirthDate)
        {
            if (newBirthDate > DateTime.Now)
                throw new ArgumentException("Date of birth cannot be in the future", nameof(newBirthDate));
            if (employeeId <= 0)
                throw new ArgumentException("Employee ID must be a positive number.", nameof(employeeId));
            var employee = await employeeRepository.GetByIdAsync(employeeId)
                ?? throw new Exception("Employee not found");

            employee.ChangeBirthDate(newBirthDate);
            await employeeRepository.UpdateAsync(employee);
        }

        public async Task ChangeFullName(int employeeId, string newFullName)
        {
            if (string.IsNullOrWhiteSpace(newFullName))
                throw new ArgumentException("Full name cannot be empty", nameof(newFullName));
            if (employeeId <= 0)
                throw new ArgumentException("Employee ID must be a positive number.", nameof(employeeId));
            var employee = await employeeRepository.GetByIdAsync(employeeId)
                ?? throw new Exception("Employee not found");

            employee.ChangeFullName(newFullName);
            await employeeRepository.UpdateAsync(employee);
        }

        public async Task ChangePosition(int employeeId, Position newPosition)
        {
            if (!Enum.IsDefined(newPosition))
                throw new ArgumentException("Invalid position", nameof(newPosition));
            if (employeeId <= 0)
                throw new ArgumentException("Employee ID must be a positive number.", nameof(employeeId));

            var employee = await employeeRepository.GetByIdAsync(employeeId)
                ?? throw new Exception("Employee not found");
            employee.ChangePosition(newPosition);
        }

        public async Task DeleteAsync(int employeeId)
        {
            if (employeeId <= 0)
                throw new ArgumentException("Employee ID must be a positive number.", nameof(employeeId));
            var employee = await employeeRepository.GetByIdAsync(employeeId)
                ?? throw new Exception("Employee not found");

            await employeeRepository.DeleteAsync(employee);
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await employeeRepository.GetAllAsync();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Employee ID must be a positive number.", nameof(id));

            return await employeeRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Employee employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(employee), "Employee cannot be null");

            await employeeRepository.UpdateAsync(employee);
        }
    }
}
