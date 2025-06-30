using TeskTask.Core.Models;

namespace TestTask.Persistence.Entities
{
    public class ContractorEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Inn { get; set; }
        public EmployeeEntity Curator { get; set; } = null!;
    }
}
