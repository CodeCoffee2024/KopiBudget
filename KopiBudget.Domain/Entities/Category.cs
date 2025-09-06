using KopiBudget.Domain.Abstractions;

namespace KopiBudget.Domain.Entities
{
    public class Category : AuditableEntity
    {
        #region Properties

        public Category()
        { }

        private Category(string name)
        {
            Name = name;
        }

        public string Name { get; set; } = string.Empty;

        // Navigation
        public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

        public virtual ICollection<Budget> Budgets { get; set; } = new List<Budget>();
        public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

        public static Category Create(string name, Guid? createdBy, DateTime? createdOn)
        {
            var entity = new Category(name);
            entity.SetCreated(createdBy!.Value, createdOn!.Value);
            return entity;
        }

        public void Update(string name, Guid? updatedBy, DateTime? updatedOn)
        {
            Name = name;
            SetUpdated(updatedBy!.Value, updatedOn!.Value);
        }

        #endregion Properties
    }
}