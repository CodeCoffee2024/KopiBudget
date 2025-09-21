using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Entities;
using System.Linq.Expressions;

namespace KopiBudget.Domain.Interfaces
{
    public interface IBudgetRepository
    {
        #region Public Methods

        Task<IEnumerable<Budget>> GetAllAsync();

        Task<bool> ExistsAsync(Guid id);
        Task<bool> ExistsByNameAsync(string name);

        Task<Budget?> GetByIdAsync(Guid id);
        Task<Budget?> GetByNameAsync(string name);

        Task<PageResult<Budget>> GetPaginatedBudgetsAsync(int page, int pageSize, string? search, string orderBy, Expression<Func<Budget, bool>>? statusFilter);

        void Remove(Budget tag);

        Task AddAsync(Budget tag);

        #endregion Public Methods
    }
}