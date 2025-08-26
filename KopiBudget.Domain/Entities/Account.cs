using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Exceptions;

namespace KopiBudget.Domain.Entities
{
    public class Account : AuditableEntity
    {
        #region Properties

        private Account(string name, decimal balance)
        {
            if (balance < 0) throw new NegativeAmountException(balance);
            Name = name;
            Balance = balance;
        }

        public string Name { get; set; } = string.Empty;
        public decimal Balance { get; set; }

        // FK
        public Guid UserId { get; set; }

        public virtual User User { get; set; } = null!;

        // Navigation
        public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

        public static Account Create(string name, decimal balance, Guid? createdBy, DateTime? createdOn)
        {
            var entity = new Account(name, balance);
            entity.SetCreated(createdBy!.Value, createdOn!.Value);
            return entity;
        }

        public void Update(string name, decimal balance, Guid? updatedById, DateTime? updatedOn)
        {
            Name = name;
            Balance = balance;
            SetUpdated(updatedById!.Value, updatedOn!.Value);
        }

        #endregion Properties
    }
}