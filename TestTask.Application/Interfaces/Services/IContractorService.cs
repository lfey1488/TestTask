using TeskTask.Core.Models;

namespace TestTask.Application.Interfaces.Services
{
    public interface IContractorService
    {
        Task ChangeName(int contractorId, string name);
        Task ChangeInn(int contractorId, int newInn);
        Task ChangeCurator(int contractorId, Employee newCurator);
    }
}
