using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Entities;
using KopiBudget.Domain.Interfaces;
using KopiBudget.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KopiBudget.Infrastructure.Repositories
{
    public class BudgetRepository : RepositoryBase<Budget>, IBudgetRepository
    {
        #region Public Constructors

        public BudgetRepository(AppDbContext context) : base(context)
        {
        }
        public async override Task<IEnumerable<Budget>> GetAllAsync()
        {
            return await _context.Budgets
                .Include(b => b.BudgetPersonalCategories)
                .ThenInclude(bpc => bpc.PersonalCategory)
                .ToListAsync();
        }
        public async Task<PageResult<Budget>> GetPaginatedBudgetsAsync(int page, int pageSize, string? search, string orderBy, Expression<Func<Budget, bool>>? statusFilter = null)
        {
            return await GetPaginatedAsync(page, pageSize, search, new[] { "Name" }, orderBy, statusFilter);
        }
        public async Task<bool> ExistsByNameAsync(string name) =>
            await _context.Set<Budget>().AnyAsync(entity => entity.Name == name);
        public async Task<Budget?> GetByNameAsync(string name) =>
            await _context.Set<Budget>().FirstOrDefaultAsync(entity => entity.Name == name);

        #endregion Public Constructors
    }
}