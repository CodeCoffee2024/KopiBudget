using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Exceptions;

namespace KopiBudget.Domain.Entities
{
    public class Transaction : AuditableEntity
    {
        #region Properties

        public Transaction()
        { }

        private Transaction(decimal amount, DateTime date, Guid categoryId, Guid accountId, string note)
        {
            if (Amount < 0) throw new NegativeAmountException(amount);
            Date = date;
            Amount = amount;
            Note = note;
            CategoryId = categoryId;
            AccountId = accountId;
            CategoryId = categoryId;
        }

        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; } = string.Empty;

        // FK
        public Guid AccountId { get; set; }

        public virtual Account Account { get; set; } = null!;

        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; } = null!;

        public static Transaction Create(decimal amount, DateTime date, Guid categoryId, Guid accountId, string note, Guid? createdBy, DateTime? createdOn)
        {
            var entity = new Transaction(amount, date, categoryId, accountId, note);
            entity.SetCreated(createdBy!.Value, createdOn!.Value);
            return entity;
        }

        public void Update(decimal amount, DateTime date, Guid categoryId, Guid accountId, Guid? updatedBy, DateTime? updatedOn)
        {
            Amount = amount;
            Date = date;
            CategoryId = categoryId;
            AccountId = accountId;
            SetUpdated(updatedBy!.Value, updatedOn!.Value);
        }

        #endregion Properties
    }
}