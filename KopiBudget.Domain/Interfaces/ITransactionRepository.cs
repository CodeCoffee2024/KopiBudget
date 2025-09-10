using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Entities;
using System.Linq.Expressions;

namespace KopiBudget.Domain.Interfaces
{
    public interface ITransactionRepository
    {
        #region Public Methods

        Task<IEnumerable<Transaction>> GetAllAsync();

        Task<bool> ExistsAsync(Guid id);

        Task<Transaction?> GetByIdAsync(Guid id);

        Task<PageResult<Transaction>> GetPaginatedCategoriesAsync(int page, int pageSize, string? search, string orderBy, Expression<Func<Transaction, bool>>? statusFilter);

        void Remove(Transaction tag);

        Task AddAsync(Transaction tag);

        #endregion Public Methods
    }
}