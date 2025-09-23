using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Entities;
using KopiBudget.Domain.Interfaces;
using KopiBudget.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<(string label, decimal value)>> GetAmountPerCategory(Guid userId)
        {
            var data = await _dbSet
                .Where(t => t.Category != null && t.CreatedById == userId) // avoid null issues
                .GroupBy(t => t.Category!.Name)
                .Select(g => new
                {
                    Label = g.Key,
                    Value = g.Sum(t => t.Amount)
                })
                .OrderByDescending(it => it.Value)
                .ToListAsync();

            return data.Select(x => (x.Label, x.Value));
        }

        public async Task<IEnumerable<(string label, decimal value)>> GetAmountPerPersonalCategory(Guid userId)
        {
            var data = await _dbSet
                .Where(t => t.PersonalCategory != null && t.CreatedById == userId) // avoid null issues
                .GroupBy(t => t.PersonalCategory!.Name)
                .Select(g => new
                {
                    Label = g.Key,
                    Value = g.Sum(t => t.Amount)
                })
                .OrderByDescending(it => it.Value)
                .ToListAsync();

            return data.Select(x => (x.Label, x.Value));
        }

        public async Task<decimal> GetBalanceAsync(int year, int month, Guid userId)
        {
            return await _dbSet
                .Where(it => it.Date.Year == year && it.Date.Month == month && it.CreatedById == userId)
                .Select(it => it.Account!.Balance)
                .SumAsync();
        }

        public async Task<decimal> GetExpenseAsync(int year, int month, Guid userId)
        {
            return await _dbSet
                .Where(it => it.Date.Year == year && it.Date.Month == month && it.CreatedById == userId)
                .Select(it => it.Amount)
                .SumAsync();
        }

        #endregion Public Constructors
    }
}