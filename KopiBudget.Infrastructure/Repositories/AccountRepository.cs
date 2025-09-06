using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Entities;
using KopiBudget.Domain.Interfaces;
using KopiBudget.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KopiBudget.Infrastructure.Repositories
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        #region Public Constructors

        public AccountRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<PageResult<Account>> GetPaginatedCategoriesAsync(int page, int pageSize, string? search, string orderBy, Expression<Func<Account, bool>>? statusFilter = null)
        {
            return await GetPaginatedAsync(page, pageSize, search, new[] { "Name" }, orderBy, statusFilter);
        }

        public virtual async Task<IEnumerable<Account>> GetAllByUserIdAsync(Guid UserId)
        {
            return await _dbSet.Where(it => it.UserId == UserId).ToListAsync();
        }

        #endregion Public Constructors
    }
}