using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Entities;
using System.Linq.Expressions;

namespace KopiBudget.Domain.Interfaces
{
    public interface IPersonalCategoryRepository
    {
        #region Public Methods

        Task<IEnumerable<PersonalCategory>> GetAllAsync(Guid userId);

        Task<bool> ExistsAsync(Guid id);

        Task<PersonalCategory?> GetByNameAsync(string name);

        Task<PersonalCategory?> GetByIdAsync(Guid id);

        Task<PageResult<PersonalCategory>> GetPaginatedPersonalCategoriesAsync(int page, int pageSize, string? search, string orderBy, Expression<Func<PersonalCategory, bool>>? statusFilter);

        Task<PageResult<PersonalCategory>> GetPaginatedPersonalCategoriesByBudgetIdAsync(int page, int pageSize, string? search, string orderBy, Guid budgetId, Guid userId);

        Task<PageResult<PersonalCategory>> GetPaginatedPersonalCategoriesByBudgetIdsAsync(int page, int pageSize, string? search, string orderBy, string budgetIds, Guid userId);

        void Remove(PersonalCategory tag);

        Task AddAsync(PersonalCategory tag);

        #endregion Public Methods
    }
}