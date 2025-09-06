using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Entities;
using System.Linq.Expressions;

namespace KopiBudget.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        #region Public Methods

        Task<IEnumerable<Category>> GetAllAsync();

        Task<bool> ExistsAsync(Guid id);

        Task<Category?> GetByIdAsync(Guid id);

        Task<PageResult<Category>> GetPaginatedCategoriesAsync(int page, int pageSize, string? search, string orderBy, Expression<Func<Category, bool>>? statusFilter);

        void Remove(Category tag);

        Task AddAsync(Category tag);

        #endregion Public Methods
    }
}