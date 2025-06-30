using TeskTask.Core.Models;

namespace TestTask.Application.Interfaces.Services
{
    public interface IContractorService : IService<Contractor>
    {
        Task ChangeName(int contractorId, string name);
        Task ChangeInn(int contractorId, int newInn);
        Task ChangeCurator(int contractorId, int employeeId);
    }
}
