using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Entities;
using System.Linq.Expressions;

namespace KopiBudget.Domain.Interfaces
{
    public interface IUserRepository
    {
        #region Public Methods

        Task<IEnumerable<User>> GetAllAsync();

        Task<bool> ExistsAsync(Guid id);

        Task<bool> HasPermission(Guid id, string moduleName, string permissionName);

        Task<bool> UsernameExists(string username);

        Task<bool> EmailExists(string email);

        Task<User?> GetByIdAsync(Guid id);

        Task<User?> EmailUsernameExists(string usernameEmail);

        Task<User?> GetByEmailAsync(string email);

        Task<User?> GetByUsernameAsync(string username);

        void Remove(User user);

        Task AddAsync(User user);

        void Update(User user);

        Task<PageResult<User>> GetPaginatedUsersAsync(int page, int pageSize, string? search, string orderBy, Expression<Func<User, bool>>? statusFilter);

        Task<int> GetNewUsersForWeekCount();

        Task<IEnumerable<User>> GetUsersByDateRangeAsync(

        DateTime dateFrom,
        DateTime dateTo,
        CancellationToken cancellationToken);

        #endregion Public Methods
    }
}