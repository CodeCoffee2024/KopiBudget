using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Exceptions;

namespace KopiBudget.Domain.Entities
{
    public class Budget : AuditableEntity
    {
        #region Properties

        public Budget()
        { }

        private Budget(decimal amount, string name, DateTime startDate, DateTime endDate, Guid? userId)
        {
            if (amount < 0)
            {
                throw new NegativeAmountException(amount);
            }
            if (endDate < startDate)
            {
                throw new InvalidDateRangeException(startDate, endDate);
            }
            Amount = amount;
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            UserId = userId!.Value;
        }

        public decimal Amount { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // FK
        public Guid UserId { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<BudgetPersonalCategory> BudgetPersonalCategories { get; set; } = new List<BudgetPersonalCategory>();
        public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

        #endregion Properties

        #region Public Methods

        public static Budget Create(decimal amount, string name, DateTime startDate, DateTime endDate, Guid? userId, Guid? createdBy, DateTime? createdOn)
        {
            var entity = new Budget(amount, name, startDate, endDate, userId);
            entity.SetCreated(createdBy!.Value, createdOn!.Value);
            return entity;
        }

        public void Update(decimal amount, string name, DateTime startDate, DateTime endDate, Guid? updatedBy, DateTime? updatedOn)
        {
            Amount = amount;
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            SetUpdated(updatedBy!.Value, updatedOn!.Value);
        }

        public void AddPersonalCategory(BudgetPersonalCategory category)
        {
            BudgetPersonalCategories.Add(category);
        }

        public decimal SpentBudget() => Transactions.Sum(it => it.Amount);

        public decimal RemainingBudget() => Amount - Transactions.Sum(it => it.Amount);

        public string SpentBudgetPercentage() => ((SpentBudget() / Amount) * 100) + "%";

        public string RemainingBudgetPercentage() => (100 - ((SpentBudget() / Amount) * 100)) + "%";

        #endregion Public Methods
    }
}