using KopiBudget.Domain.Abstractions;
using KopiBudget.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace KopiBudget.Infrastructure.Repositories
{
    public static class PredicateBuilder
    {
        #region Public Methods

        public static Expression<Func<T, bool>> True<T>() => x => true;

        public static Expression<Func<T, bool>> False<T>() => x => false;

        public static Expression<Func<T, bool>> Or<T>(
            this Expression<Func<T, bool>> expr1,
            Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters);
            return Expression.Lambda<Func<T, bool>>(
                Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(
            this Expression<Func<T, bool>> expr1,
            Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters);
            return Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }

        #endregion Public Methods
    }

    public abstract class RepositoryBase<TEntity> where TEntity : class
    {
        #region Fields

        protected readonly AppDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        #endregion Fields

        #region Protected Constructors

        protected RepositoryBase(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        #endregion Protected Constructors

        #region Public Methods

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<TEntity?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public virtual async Task<bool> ExistsAsync(Guid id)
        {
            // Assumes all entities have a property named "Id" of type Guid
            return await _dbSet.AnyAsync(e =>
                EF.Property<Guid>(e, "Id") == id);
        }

        public virtual async Task<PageResult<TEntity>> GetPaginatedAsync(
            int page,
            int pageSize,
            string? search = null,
            string[]? searchableProperties = null,
            string? orderBy = null,
            Expression<Func<TEntity, bool>>? additionalFilter = null,
            string[]? includes = null)
        {
            var query = _dbSet.AsQueryable();

            // Include navigation properties if any
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            if (!string.IsNullOrWhiteSpace(search) && searchableProperties != null)
            {
                var predicate = PredicateBuilder.False<TEntity>();

                foreach (var prop in searchableProperties)
                {
                    string filter;

                    // Handle navigation (Tags.Name or Category.Name)
                    if (prop.Contains("."))
                    {
                        var parts = prop.Split('.');
                        var first = parts[0];
                        var rest = string.Join(".", parts.Skip(1));

                        // Treat collections as .Any(...) — assume plural names (convention-based)
                        if (IsCollectionNavigation(first))
                        {
                            filter = $"{first}.Any({rest}.Contains(@0))";
                        }
                        else
                        {
                            filter = $"{prop}.Contains(@0)";
                        }
                    }
                    else
                    {
                        filter = $"{prop}.Contains(@0)";
                    }

                    var expr = DynamicExpressionParser.ParseLambda<TEntity, bool>(
                        new ParsingConfig(), false, filter, search);

                    predicate = predicate.Or(expr);
                }

                query = query.Where(predicate);
            }

            if (additionalFilter != null)
            {
                query = query.Where(additionalFilter);
            }

            var totalCount = await query.CountAsync();

            if (string.IsNullOrWhiteSpace(orderBy))
                orderBy = "Id";

            var items = await query
                .OrderBy(orderBy)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageResult<TEntity>(items, totalCount, page, pageSize, orderBy);
        }

        private bool IsCollectionNavigation(string propertyName)
        {
            var prop = typeof(TEntity).GetProperty(propertyName);
            if (prop == null) return false;

            return typeof(System.Collections.IEnumerable).IsAssignableFrom(prop.PropertyType) &&
                   prop.PropertyType != typeof(string);
        }

        #endregion Public Methods
    }
}