namespace TestTask.Persistence.Entities
{
    public class ContractorEntity
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; } = string.Empty;
        public virtual int Inn { get; set; }
        public virtual EmployeeEntity Curator { get; set; } = null!;
    }
}
