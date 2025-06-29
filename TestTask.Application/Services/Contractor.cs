using TestTask.Application.Interfaces.Repositories;
using TestTask.Application.Interfaces.Services;

namespace TestTask.Application.Services
{
    public class Contractor : IService<Contractor>
    {
        private readonly IRepository<Contractor> contractorRepository;
        public Contractor(IRepository<Contractor> contractorRepository)
        {
            this.contractorRepository = contractorRepository;
        }

        public Task AddAsync(Contractor employee)
        {
            return contractorRepository.AddAsync(employee);
        }

        public Task DeleteAsync(Contractor employee)
        {
            return contractorRepository.DeleteAsync(employee);
        }

        public Task<IEnumerable<Contractor>> GetAllAsync()
        {
            return contractorRepository.GetAllAsync(); 
        }

        public Task<Contractor?> GetByIdAsync(int id)
        {
            return contractorRepository.GetByIdAsync(id);
        }

        public Task UpdateAsync(Contractor employee)
        {
            return contractorRepository.UpdateAsync(employee);
        }
    }
}
