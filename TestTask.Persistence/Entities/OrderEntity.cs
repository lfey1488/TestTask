namespace TestTask.Persistence.Entities
{
    public class OrderEntity
    {
        public virtual int Id { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual decimal Amount { get; set; }
        public virtual EmployeeEntity Employee { get; set; } = null!;
        public virtual ContractorEntity Contractor { get; set; } = null!;
    }
}
