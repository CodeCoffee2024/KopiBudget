using KopiBudget.Domain.Entities;

namespace KopiBudget.Domain.Interfaces
{
    public interface IBudgetPersonalCategoryRepository
    {
        #region Public Methods

        Task<BudgetPersonalCategory?> GetByBudgetIdAndPersonalCategoryIdAsync(Guid budgetId, Guid personalCategoryId);

        #endregion Public Methods
    }
}