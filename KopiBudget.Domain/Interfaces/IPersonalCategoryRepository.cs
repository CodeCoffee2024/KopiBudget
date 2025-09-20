using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Entities;
using System.Linq.Expressions;

namespace KopiBudget.Domain.Interfaces
{
    public interface IPersonalCategoryRepository
    {
        #region Public Methods

        Task<IEnumerable<PersonalCategory>> GetAllAsync();

        Task<bool> ExistsAsync(Guid id);

        Task<PersonalCategory?> GetByIdAsync(Guid id);

        Task<PageResult<PersonalCategory>> GetPaginatedPersonalCategoriesAsync(int page, int pageSize, string? search, string orderBy, Expression<Func<PersonalCategory, bool>>? statusFilter);

        Task<PageResult<PersonalCategory>> GetPaginatedPersonalCategoriesByBudgetIdAsync(int page, int pageSize, string? search, string orderBy, Guid budgetId);

        Task<PageResult<PersonalCategory>> GetPaginatedPersonalCategoriesByBudgetIdsAsync(int page, int pageSize, string? search, string orderBy, string budgetIds);

        void Remove(PersonalCategory tag);

        Task AddAsync(PersonalCategory tag);

        #endregion Public Methods
    }
}