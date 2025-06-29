using TeskTask.Core.Models;
using TestTask.Application.Interfaces.Repositories;
using TestTask.Application.Interfaces.Services;

namespace TestTask.Application.Services
{
    public class ContractorService : IService<Contractor>, IContractorService
    {
        private readonly IRepository<Contractor> contractorRepository;
        private readonly IRepository<Employee> employeeRepository;
        public ContractorService(IRepository<Contractor> contractorRepository, IRepository<Employee> employeeService)
        {
            this.contractorRepository = contractorRepository;
            this.employeeRepository = employeeService;
        }

        public async Task AddAsync(Contractor employee)
        {
            await contractorRepository.AddAsync(employee);
        }

        public async Task ChangeCurator(int contractorId, Employee newCurator)
        {
            if (newCurator is null)
                throw new ArgumentNullException(nameof(newCurator), "New curator cannot be null");
            if (contractorId <= 0)
                throw new ArgumentException("Contractor ID must be a positive number.", nameof(contractorId));

            var contractor = await contractorRepository.GetByIdAsync(contractorId)
                ?? throw new Exception("Contractor not found");

            contractor.ChangeCurator(newCurator);
            await contractorRepository.UpdateAsync(contractor);
        }

        public async Task ChangeInn(int contractorId, int newInn)
        {
            if (newInn <= 0)
                throw new ArgumentException("INN must be a positive number.", nameof(newInn));
            if (contractorId <= 0)
                throw new ArgumentException("Contractor ID must be a positive number.", nameof(contractorId));

            var contractor = await contractorRepository.GetByIdAsync(contractorId)
                ?? throw new Exception("Contractor not found");

            contractor.ChangeInn(newInn);
            await contractorRepository.UpdateAsync(contractor);
        }

        public async Task ChangeName(int contractorId, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("The name cannot be empty", nameof(name));
            if (contractorId <= 0)
                throw new ArgumentException("Contractor ID must be a positive number.", nameof(contractorId));

            var contractor = await contractorRepository.GetByIdAsync(contractorId)
                ?? throw new Exception("Contractor not found");

            contractor.ChangeName(name);
            await contractorRepository.UpdateAsync(contractor);
        }

        public async Task DeleteAsync(Contractor employee)
        {
            await contractorRepository.DeleteAsync(employee);
        }

        public async Task<IEnumerable<Contractor>> GetAllAsync()
        {
            return await contractorRepository.GetAllAsync(); 
        }

        public async Task<Contractor?> GetByIdAsync(int id)
        {
            return await contractorRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Contractor employee)
        {
            await contractorRepository.UpdateAsync(employee);
        }
    }
}
