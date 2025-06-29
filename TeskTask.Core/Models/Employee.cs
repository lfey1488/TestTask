using TeskTask.Core.Enums;

namespace TeskTask.Core.Models
{
    public class Employee
    {
        public int Id { get; private set; }
        public string FullName { get; private set; }
        public Position Position { get; private set; }
        public DateOnly BirthDate { get; private set; }

        public Employee(string fullName, Position position, DateOnly birthDate)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("Full name cannot be empty", nameof(fullName));
            if (birthDate.ToDateTime(TimeOnly.MinValue) > DateTime.Now)
                throw new ArgumentException("Date of birth cannot be in the future", nameof(birthDate));

            FullName = fullName;
            Position = position;
            BirthDate = birthDate;
        }

        public void ChangePosition(Position newPosition)
        {
            Position = newPosition;
        }
    }
}
