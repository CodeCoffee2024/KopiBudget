using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Entities;
using System.Linq.Expressions;

namespace KopiBudget.Domain.Interfaces
{
    public interface IRoleRepository
    {
        #region Public Methods

        Task<IEnumerable<Role>> GetAllAsync();

        Task<bool> ExistsAsync(Guid id);

        Task<bool> RoleExists(string Rolename);

        Task<Role?> GetByIdAsync(Guid id);

        Task<Role?> GetByNameAsync(string Rolename);

        void Remove(Role Role);

        Task AddAsync(Role Role);

        void Update(Role Role);

        Task<PageResult<Role>> GetPaginatedRolesAsync(int page, int pageSize, string? search, string orderBy, Expression<Func<Role, bool>>? statusFilter);

        #endregion Public Methods
    }
}