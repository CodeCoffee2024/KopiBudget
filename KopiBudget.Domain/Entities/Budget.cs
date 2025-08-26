using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Exceptions;

namespace KopiBudget.Domain.Entities
{
    public class Budget : AuditableEntity
    {
        #region Properties

        private Budget()
        { }

        private Budget(decimal amount, string name, DateTime startDate, DateTime endDate, Guid? userId, Guid? categoryId)
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
            CategoryId = categoryId!.Value;
            UserId = userId!.Value;
        }

        public decimal Amount { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // FK
        public Guid UserId { get; set; }

        public virtual User User { get; set; } = null!;

        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; } = null!;

        #endregion Properties

        #region Public Methods

        public static Budget Create(decimal amount, string name, DateTime startDate, DateTime endDate, Guid? userId, Guid? categoryId, Guid? createdBy, DateTime? createdOn)
        {
            var entity = new Budget(amount, name, startDate, endDate, userId, categoryId);
            entity.SetCreated(createdBy!.Value, createdOn!.Value);
            return entity;
        }

        public void Update(decimal amount, string name, DateTime startDate, DateTime endDate, Guid? userId, Guid? categoryId, Guid? updatedBy, DateTime? updatedOn)
        {
            Amount = amount;
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            CategoryId = categoryId!.Value;
            UserId = userId!.Value;
            SetUpdated(updatedBy!.Value, updatedOn!.Value);
        }

        #endregion Public Methods
    }
}