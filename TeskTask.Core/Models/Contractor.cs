namespace TeskTask.Core.Models
{
    public class Contractor
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int Inn { get; private set; }
        public int CuratorId { get; private set; }

        private Contractor(string name, int inn, int curatorId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("The name cannot be empty", nameof(name));
            if (inn < 0)
                throw new ArgumentException("INN must be a positive number", nameof(inn));
            if (curatorId <= 0)
                throw new ArgumentException("Curator ID must be a positive number", nameof(curatorId));

            CuratorId = curatorId;
            Name = name;
            Inn = inn;
        }

        public static Contractor Create(string name, int inn, int curatorId)
        {
            return new Contractor(name, inn, curatorId);
        }

        public void ChangeName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("The name cannot be empty", nameof(newName));

            Name = newName;
        }
        public void ChangeInn(int newInn)
        {
            if (newInn <= 0)
                throw new ArgumentException("INN must be a positive number", nameof(newInn));

            Inn = newInn;
        }
        public void ChangeCurator(int newCuratorId)
        {
            if (newCuratorId <= 0)
                throw new ArgumentException("Curator ID must be a positive number", nameof(newCuratorId));

            CuratorId = newCuratorId;
        }
    }
}