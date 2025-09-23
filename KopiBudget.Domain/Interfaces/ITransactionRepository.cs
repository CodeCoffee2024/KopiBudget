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

        Task<decimal> GetBalanceAsync(int year, int month, Guid userId);

        Task<decimal> GetExpenseAsync(int year, int month, Guid userId);

        Task<Transaction?> GetByIdAsync(Guid id);

        Task<IEnumerable<(string label, decimal value)>> GetAmountPerCategory(Guid userId);

        Task<IEnumerable<(string label, decimal value)>> GetAmountPerPersonalCategory(Guid userId);

        Task<PageResult<Transaction>> GetPaginatedCategoriesAsync(int page, int pageSize, string? search, string orderBy, Expression<Func<Transaction, bool>>? statusFilter);

        void Remove(Transaction tag);

        Task AddAsync(Transaction tag);

        #endregion Public Methods
    }
}