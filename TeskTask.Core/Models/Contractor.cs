namespace TeskTask.Core.Models
{
    public class Contractor
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int Inn { get; private set; }
        public Employee Curator { get; private set; }

        public Contractor(string name, int inn, Employee curator)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("The name cannot be empty", nameof(name));
            if (inn <= 0)
                throw new ArgumentException("INN must be a positive number", nameof(inn));
            Curator = curator ?? throw new ArgumentNullException(nameof(curator));

            Name = name;
            Inn = inn;
        }

        public void ChangeCurator(Employee newCurator)
        {
            Curator = newCurator ?? throw new ArgumentNullException(nameof(newCurator));
        }

        public void Rename(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("The name cannot be empty", nameof(newName));
            Name = newName;
        }
    }
}