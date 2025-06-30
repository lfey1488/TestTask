using TeskTask.Core.Enums;

namespace TestTask.Persistence.Entities
{
    public class EmployeeEntity
    {
        public virtual int Id { get; set; }
        public virtual string FullName { get; set; } = string.Empty;
        public virtual Position Position { get; set; }
        public virtual DateTime BirthDate { get; set; }
    }
}
