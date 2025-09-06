using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Entities;
using KopiBudget.Domain.Interfaces;
using KopiBudget.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KopiBudget.Infrastructure.Repositories
{
    public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        #region Public Constructors

        public RoleRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Role?> GetByNameAsync(string Rolename) =>
            await _context.Set<Role>().FirstOrDefaultAsync(Role => Role.Name == Rolename);

        public async Task<bool> RoleExists(string name) =>
            await _context.Set<Role>().AnyAsync(Role => Role.Name == name);

        public async Task<PageResult<Role>> GetPaginatedRolesAsync(int page, int pageSize, string? search, string orderBy, Expression<Func<Role, bool>>? statusFilter = null)
        {
            return await GetPaginatedAsync(page, pageSize, search, new[] { "Name" }, orderBy, statusFilter);
        }

        #endregion Public Constructors
    }
}