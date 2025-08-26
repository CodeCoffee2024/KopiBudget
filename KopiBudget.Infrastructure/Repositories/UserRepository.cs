using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Entities;
using KopiBudget.Domain.Interfaces;
using KopiBudget.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KopiBudget.Infrastructure.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        #region Public Constructors

        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<User?> GetByEmailAsync(string email) =>
            await _context.Set<User>().FirstOrDefaultAsync(user => user.Email == email);

        public async Task<User?> GetByUsernameAsync(string username) =>
            await _context.Set<User>().FirstOrDefaultAsync(user => user.UserName == username);

        public async Task<User?> EmailUsernameExists(string usernameEmail) =>
            await _context.Set<User>().FirstOrDefaultAsync(user => user.Email == usernameEmail || user.UserName == usernameEmail);

        public async Task<bool> EmailExists(string email) =>
            await _context.Set<User>().AnyAsync(user => user.Email == email);

        public async Task<bool> UsernameExists(string username) =>
            await _context.Set<User>().AnyAsync(user => user.UserName == username);

        public async Task<bool> HasPermission(Guid userId, string moduleName, string permissionName) =>
            await _context.Set<User>()
            .Where(u => u.Id == userId)
            .SelectMany(u => u.UserRoles)
            .SelectMany(ur => ur.Role!.RolePermissions)
            .AnyAsync(rp =>
                rp.Permission.Name == permissionName &&
                rp.Permission.Module!.Name == moduleName);

        public async Task<PageResult<User>> GetPaginatedUsersAsync(int page, int pageSize, string? search, string orderBy, Expression<Func<User, bool>>? statusFilter = null)
        {
            return await GetPaginatedAsync(page, pageSize, search, new[] { "Email", "Status", "UserName", "FirstName", "MiddleName", "LastName", "UserRoles.Role.Name" }, orderBy, statusFilter);
        }

        public async Task<int> GetNewUsersForWeekCount()
        {
            var startOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
            var endOfWeek = startOfWeek.AddDays(7);

            return await _context.Set<User>()
                .Where(it => it.CreatedOn >= startOfWeek && it.CreatedOn < endOfWeek)
                .CountAsync();
        }

        public async Task<IEnumerable<User>> GetUsersByDateRangeAsync(
            DateTime dateFrom,
            DateTime dateTo,
            CancellationToken cancellationToken)
        {
            var from = new DateTime(dateFrom.Year, dateFrom.Month, 1, 0, 0, 0);

            var lastDay = DateTime.DaysInMonth(dateTo.Year, dateTo.Month);
            var to = new DateTime(dateTo.Year, dateTo.Month, lastDay, 23, 59, 59);

            return await _context.Set<User>()
                .Where(u => u.CreatedOn >= from && u.CreatedOn <= to)
                .ToListAsync(cancellationToken);
        }

        #endregion Public Constructors
    }
}