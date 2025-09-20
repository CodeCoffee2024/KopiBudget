using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Entities;
using KopiBudget.Domain.Interfaces;
using KopiBudget.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace KopiBudget.Infrastructure.Repositories
{
    public class PersonalCategoryRepository : RepositoryBase<PersonalCategory>, IPersonalCategoryRepository
    {
        #region Public Constructors

        public PersonalCategoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<PageResult<PersonalCategory>> GetPaginatedPersonalCategoriesAsync(int page, int pageSize, string? search, string orderBy, Expression<Func<PersonalCategory, bool>>? statusFilter = null)
        {
            return await GetPaginatedAsync(page, pageSize, search, new[] { "Name" }, orderBy, statusFilter);
        }

        public async Task<PageResult<PersonalCategory>> GetPaginatedPersonalCategoriesByBudgetIdAsync(
             int page,
             int pageSize,
             string? search,
             string orderBy,
             Guid budgetId)
        {
            var query = _context.Set<BudgetPersonalCategory>()
                .Where(it => it.BudgetId == budgetId)
                .Select(it => it.PersonalCategory);

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(pc => pc.Name.ToLower().Contains(search));
            }

            // Total count before paging
            var totalCount = await query.CountAsync();

            // Paging + sorting
            var items = await query
                .OrderBy(orderBy) // using System.Linq.Dynamic.Core
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageResult<PersonalCategory>(items!, totalCount, page, pageSize, orderBy);
        }

        public async Task<PageResult<PersonalCategory>> GetPaginatedPersonalCategoriesByBudgetIdsAsync(
             int page,
             int pageSize,
             string? search,
             string orderBy,
             string guids)
        {
            var query = _context.Set<BudgetPersonalCategory>()
                .Where(it => guids.Contains(it.BudgetId.ToString()))
                .Select(it => it.PersonalCategory);

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(pc => pc.Name.ToLower().Contains(search));
            }

            // Total count before paging
            var totalCount = await query.CountAsync();

            // Paging + sorting
            var items = await query
                .OrderBy(orderBy) // using System.Linq.Dynamic.Core
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageResult<PersonalCategory>(items!, totalCount, page, pageSize, orderBy);
        }

        #endregion Public Constructors
    }
}