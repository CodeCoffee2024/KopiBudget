using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Entities;
using System.Linq.Expressions;

namespace KopiBudget.Domain.Interfaces
{
    public interface IAccountRepository
    {
        #region Public Methods

        Task<IEnumerable<Account>> GetAllAsync();

        Task<IEnumerable<Account>> GetAllByUserIdAsync(Guid UserId);

        Task<bool> ExistsAsync(Guid id);

        Task<Account?> GetByIdAsync(Guid id);

        Task<PageResult<Account>> GetPaginatedCategoriesAsync(int page, int pageSize, string? search, string orderBy, Expression<Func<Account, bool>>? statusFilter);

        void Remove(Account tag);

        Task AddAsync(Account tag);

        #endregion Public Methods
    }
}