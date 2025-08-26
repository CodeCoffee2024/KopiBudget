using KopiBudget.Domain.Abstractions;

namespace KopiBudget.Domain.Entities
{
    public class Transaction : AuditableEntity
    {
        #region Properties

        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; } = string.Empty;

        // FK
        public Guid AccountId { get; set; }

        public virtual Account Account { get; set; } = null!;

        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; } = null!;

        #endregion Properties
    }
}