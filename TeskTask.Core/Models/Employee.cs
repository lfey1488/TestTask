using TeskTask.Core.Enums;

namespace TeskTask.Core.Models
{
    public class Employee
    {
        public int Id { get; private set; }
        public string FullName { get; private set; }
        public Position Position { get; private set; }
        public DateTime BirthDate { get; private set; }

        public Employee(string fullName, Position position, DateTime birthDate)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("Full name cannot be empty", nameof(fullName));
            if (birthDate > DateTime.Now)
                throw new ArgumentException("Date of birth cannot be in the future", nameof(birthDate));

            FullName = fullName;
            Position = position;
            BirthDate = birthDate;
        }

        public void ChangeFullName(string newFullName)
        {
            if (string.IsNullOrWhiteSpace(newFullName))
                throw new ArgumentException("Full name cannot be empty", nameof(newFullName));

            FullName = newFullName;
        }
        public void ChangePosition(Position newPosition)
        {
            if (!Enum.IsDefined(newPosition))
                throw new ArgumentException("Invalid position", nameof(newPosition));

            Position = newPosition;
        }
        public void ChangeBirthDate(DateTime newBirthDate)
        {
            if (newBirthDate > DateTime.Now)
                throw new ArgumentException("Date of birth cannot be in the future", nameof(newBirthDate));

            BirthDate = newBirthDate;
        }
    }
}
