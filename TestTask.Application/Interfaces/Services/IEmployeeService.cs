using TeskTask.Core.Enums;
using TeskTask.Core.Models;

namespace TestTask.Application.Interfaces.Services
{
    public interface IEmployeeService : IService<Employee>
    {
        Task ChangeFullName(int employeeId, string newFullName);
        Task ChangePosition(int employeeId, Position newPosition);
        Task ChangeBirthDate(int employeeId, DateTime newBirthDate);
    }
}
