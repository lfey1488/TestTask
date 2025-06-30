using TeskTask.Core.Models;

namespace TestTask.Persistence.Entities
{
    public class OrderEntity
    {
        public int Id { get; private set; }
        public DateTime Date { get; private set; }
        public decimal Amount { get; private set; }
        public EmployeeEntity Employee { get; private set; } = null!;
        public ContractorEntity Contractor { get; private set; } = null!;
    }
}
