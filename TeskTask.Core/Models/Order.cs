namespace TeskTask.Core.Models
{
    public class Order
    {
        public int Id { get; private set; }
        public DateTime Date { get; private set; }
        public decimal Amount { get; private set; }
        public Employee Employee { get; private set; }
        public Contractor Contractor { get; private set; }

        public Order(DateTime date, decimal amount, Employee employee, Contractor contractor)
        {
            if (amount < 0)
                throw new ArgumentException("The amount cannot be negative.", nameof(amount));
            Employee = employee ?? throw new ArgumentNullException(nameof(employee));
            Contractor = contractor ?? throw new ArgumentNullException(nameof(contractor));
            Date = date;
            Amount = amount;
        }

        public void ChangeAmount(decimal newAmount)
        {
            if (newAmount < 0)
                throw new ArgumentException("The amount cannot be negative.", nameof(newAmount));
            Amount = newAmount;
        }

        public void ChangeEmployee(Employee newEmployee)
        {
            Employee = newEmployee ?? throw new ArgumentNullException(nameof(newEmployee));
        }

        public void ChangeContractor(Contractor newContractor)
        {
            Contractor = newContractor ?? throw new ArgumentNullException(nameof(newContractor));
        }
    }
}