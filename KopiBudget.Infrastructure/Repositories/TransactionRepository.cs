using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Entities;
using KopiBudget.Domain.Interfaces;
using KopiBudget.Infrastructure.Data;
using System.Linq.Expressions;

namespace KopiBudget.Infrastructure.Repositories
{
    public class TransactionRepository : RepositoryBase<Transaction>, ITransactionRepository
    {
        #region Public Constructors

        public TransactionRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<PageResult<Transaction>> GetPaginatedCategoriesAsync(int page, int pageSize, string? search, string orderBy, Expression<Func<Transaction, bool>>? statusFilter = null)
        {
            return await GetPaginatedAsync(page, pageSize, search, new[] { "Account.Name" }, orderBy, statusFilter);
        }

        #endregion Public Constructors
    }
}