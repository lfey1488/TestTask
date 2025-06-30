namespace TeskTask.Core.Models
{
    public class Order
    {
        public int Id { get; private set; }
        public DateTime Date { get; private set; }
        public decimal Amount { get; private set; }
        public int EmployeeId { get; private set; }
        public int ContractorId { get; private set; }

        public Order(DateTime date, decimal amount, int employeeId, int contractorId)
        {
            if (amount < 0)
                throw new ArgumentException("The amount cannot be negative.", nameof(amount));
            if (date > DateTime.Now)
                throw new ArgumentException("The date cannot be in the future.", nameof(date));
            if (employeeId <= 0)
                throw new ArgumentException("Employee ID must be a positive number.", nameof(employeeId));
            if (contractorId <= 0)
                throw new ArgumentException("Contractor ID must be a positive number.", nameof(contractorId));

            EmployeeId = employeeId;
            ContractorId = contractorId;
            Date = date;
            Amount = amount;
        }

        public void ChangeDate(DateTime newDate)
        {
            if (newDate > DateTime.Now)
                throw new ArgumentException("The date cannot be in the future.", nameof(newDate));

            Date = newDate;
        }
        public void ChangeAmount(decimal newAmount)
        {
            if (newAmount < 0)
                throw new ArgumentException("The amount cannot be negative.", nameof(newAmount));

            Amount = newAmount;
        }

        public void ChangeEmployee(int newEmployeeId)
        {
            if (newEmployeeId <= 0)
                throw new ArgumentException("Employee ID must be a positive number.", nameof(newEmployeeId));

            EmployeeId = newEmployeeId;
        }

        public void ChangeContractor(int newContractorId)
        {
            if (newContractorId <= 0)
                throw new ArgumentException("Contractor ID must be a positive number.", nameof(newContractorId));
            ContractorId = newContractorId;
        }
    }
}