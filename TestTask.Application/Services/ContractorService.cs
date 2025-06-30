using TeskTask.Core.Models;
using TestTask.Application.Interfaces.Repositories;
using TestTask.Application.Interfaces.Services;

namespace TestTask.Application.Services
{
    public class ContractorService : IContractorService
    {
        private readonly IRepository<Contractor> contractorRepository;
        private readonly IRepository<Employee> employeeRepository;
        public ContractorService(IRepository<Contractor> contractorRepository, IRepository<Employee> employeeRepository)
        {
            this.contractorRepository = contractorRepository;
            this.employeeRepository = employeeRepository;
        }

        public async Task AddAsync(Contractor employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(employee), "Contractor cannot be null");

            await contractorRepository.AddAsync(employee);
        }

        public async Task ChangeCurator(int contractorId, int newCuratorId)
        {
            if (newCuratorId <= 0)
                throw new ArgumentException("Curator ID must be a positive number.", nameof(newCuratorId));
            if (contractorId <= 0)
                throw new ArgumentException("Contractor ID must be a positive number.", nameof(contractorId));

            var contractor = await contractorRepository.GetByIdAsync(contractorId)
                ?? throw new Exception("Contractor not found");
            var newCurator = await employeeRepository.GetByIdAsync(newCuratorId)
                ?? throw new Exception("Curator not found");

            contractor.ChangeCurator(newCurator.Id);
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

        public async Task DeleteAsync(int contractorId)
        {
            if (contractorId <= 0)
                throw new ArgumentException("Employee ID must be a positive number.", nameof(contractorId));

            var contractor = await contractorRepository.GetByIdAsync(contractorId)
                ?? throw new Exception("Contractor not found");

            await contractorRepository.DeleteAsync(contractor);
        }

        public async Task<IEnumerable<Contractor>> GetAllAsync()
        {
            return await contractorRepository.GetAllAsync(); 
        }

        public async Task<Contractor?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Contractor ID must be a positive number.", nameof(id));

            return await contractorRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Contractor employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(employee), "Contractor cannot be null");

            await contractorRepository.UpdateAsync(employee);
        }
    }
}
