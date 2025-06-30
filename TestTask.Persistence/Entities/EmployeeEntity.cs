using TeskTask.Core.Enums;

namespace TestTask.Persistence.Entities
{
    public class EmployeeEntity
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public Position Position { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
