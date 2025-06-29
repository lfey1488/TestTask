using TeskTask.Core.Enums;

namespace TestTask.Application.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task ChangeFullName(int employeeId, string newFullName);
        Task ChangePosition(int employeeId, Position newPosition);
        Task ChangeBirthDate(int employeeId, DateTime newBirthDate);
    }
}
