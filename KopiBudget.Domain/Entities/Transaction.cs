using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Exceptions;

namespace KopiBudget.Domain.Entities
{
    public static class TransactionTypes
    {
        #region Fields

        public const string Account = "accou";
        public const string Budget = "budge";

        #endregion Fields
    }

    public class Transaction : AuditableEntity
    {
        #region Properties

        public Transaction()
        { }

        private Transaction(decimal amount, DateTime date, Guid? categoryId, Guid? accountId, Guid? budgetId, Guid? personalCategoryId, string note, string type)
        {
            if (Amount < 0) throw new NegativeAmountException(amount);
            Date = date;
            Amount = amount;
            Note = note;
            if (type == TransactionTypes.Account)
            {
                CategoryId = categoryId;
                AccountId = accountId;
            }
            if (type == TransactionTypes.Budget)
            {
                BudgetId = budgetId;
                PersonalCategoryId = personalCategoryId;
            }
        }

        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; } = string.Empty;

        // FK
        public Guid? AccountId { get; set; }

        public virtual Account? Account { get; set; } = null!;

        public Guid? CategoryId { get; set; }
        public virtual Category? Category { get; set; } = null!;
        public Guid? BudgetId { get; set; }
        public virtual Budget? Budget { get; set; } = null!;

        public Guid? PersonalCategoryId { get; set; }
        public virtual PersonalCategory? PersonalCategory { get; set; } = null!;

        public static Transaction Create(decimal amount, DateTime date, Guid? categoryId, Guid? accountId, Guid? budgetId, Guid? personalCategoryId, string note, string type, Guid? createdBy, DateTime? createdOn)
        {
            var entity = new Transaction(amount, date, categoryId, accountId, budgetId, personalCategoryId, type, note);
            entity.SetCreated(createdBy!.Value, createdOn!.Value);
            return entity;
        }

        public void Update(decimal amount, DateTime date, string note, Guid? updatedBy, DateTime? updatedOn)
        {
            Date = date;
            Amount = amount;
            Note = note;
            SetUpdated(updatedBy!.Value, updatedOn!.Value);
        }

        #endregion Properties
    }
}