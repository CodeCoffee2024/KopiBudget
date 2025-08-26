using KopiBudget.Domain.Abstractions;

namespace KopiBudget.Domain.Entities
{
    public class Category : AuditableEntity
    {
        #region Properties

        private Category(string name, bool isExpense)
        {
            Name = name;
            IsExpense = isExpense;
        }

        public string Name { get; set; } = string.Empty;
        public bool IsExpense { get; set; }

        // FK
        public Guid UserId { get; set; }

        public virtual User User { get; set; } = null!;

        // Navigation
        public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

        public virtual ICollection<Budget> Budgets { get; set; } = new List<Budget>();

        public static Category Create(string name, bool isExpense, Guid? createdBy, DateTime? createdOn)
        {
            var entity = new Category(name, isExpense);
            entity.SetCreated(createdBy!.Value, createdOn!.Value);
            return new Category(name, isExpense);
        }

        public void Update(string name, bool isExpense, Guid? updatedBy, DateTime? updatedOn)
        {
            Name = name;
            IsExpense = isExpense;
            SetUpdated(updatedBy!.Value, updatedOn!.Value);
        }

        #endregion Properties
    }
}