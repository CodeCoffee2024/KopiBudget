using KopiBudget.Domain.Exceptions;

namespace KopiBudget.Domain.Entities
{
    public record class BudgetPersonalCategory
    {
        public Guid PersonalCategoryId { get; set; }
        public virtual PersonalCategory? PersonalCategory { get; set; }

        public Guid BudgetId { get; set; }
        public virtual Budget? Budget { get; set; }
        public decimal? TransactionAmount { get; set; }
        public decimal? Limit { get; set; }

        public BudgetPersonalCategory() { }
        private BudgetPersonalCategory(Guid personalCategoryId, Guid budgetId, decimal? limit, decimal? transactionAmount)
        {
            PersonalCategoryId = personalCategoryId;
            TransactionAmount = transactionAmount;
            BudgetId = budgetId;
            Limit = limit;
        }
        public static BudgetPersonalCategory Create(Guid personalCategoryId, Guid budgetId, decimal? limit, decimal? transactionAmount)
        {
            return new BudgetPersonalCategory(personalCategoryId, budgetId, limit, transactionAmount);
        }

        public void AddToTransactionAmount(decimal transactionAmount)
        {
            TransactionAmount += transactionAmount;
        }
        public void UpdateTransactionAmount(decimal transactionAmount, bool isNew = false)
        {
            if (isNew)
            {
                TransactionAmount = transactionAmount;
            }
            else
            {
                if (TransactionAmount - transactionAmount < 0)
                {
                    throw new NegativeAmountException(TransactionAmount!.Value - transactionAmount);
                }
                TransactionAmount -= transactionAmount;
            }
        }
        public decimal? SpentBudget() => TransactionAmount ?? 0;
        public decimal? RemainingBudget() => Limit ?? 0 - SpentBudget();
        public string SpentBudgetPercentage() => ((SpentBudget() / Limit ?? 0) * 100) + "%";
        public string RemainingBudgetPercentage() => (100 - ((SpentBudget() / Limit ?? 0) * 100)) + "%";
    }
}