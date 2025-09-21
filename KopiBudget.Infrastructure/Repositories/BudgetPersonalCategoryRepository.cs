using KopiBudget.Domain.Entities;
using KopiBudget.Domain.Interfaces;
using KopiBudget.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace KopiBudget.Infrastructure.Repositories
{
    public class BudgetPersonalCategoryRepository : RepositoryBase<BudgetPersonalCategory>, IBudgetPersonalCategoryRepository
    {
        #region Public Constructors

        public BudgetPersonalCategoryRepository(AppDbContext context) : base(context)
        {
        }

        public virtual async Task<BudgetPersonalCategory?> GetByBudgetIdAndPersonalCategoryIdAsync(Guid BudgetId, Guid PersonalCategoryId)
        {
            return await _dbSet.Include(it => it.Budget).Where(it => it.BudgetId == BudgetId && it.PersonalCategoryId == PersonalCategoryId).FirstOrDefaultAsync();
        }

        #endregion Public Constructors
    }
}