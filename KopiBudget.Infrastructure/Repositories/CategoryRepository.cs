using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Entities;
using KopiBudget.Domain.Interfaces;
using KopiBudget.Infrastructure.Data;
using System.Linq.Expressions;

namespace KopiBudget.Infrastructure.Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        #region Public Constructors

        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<PageResult<Category>> GetPaginatedCategoriesAsync(int page, int pageSize, string? search, string orderBy, Expression<Func<Category, bool>>? statusFilter = null)
        {
            return await GetPaginatedAsync(page, pageSize, search, new[] { "Name" }, orderBy, statusFilter);
        }

        #endregion Public Constructors
    }
}