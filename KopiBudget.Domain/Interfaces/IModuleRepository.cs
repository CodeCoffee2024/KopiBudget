using KopiBudget.Domain.Abstractions;
using KopiBudget.Domain.Entities;

namespace KopiBudget.Domain.Interfaces
{
    public interface IModuleRepository
    {
        #region Public Methods

        Task<bool> ExistsAsync(Guid id);

        Task<bool> ExistsByNameAsync(string name);

        Task<Module?> GetByIdAsync(Guid id);

        Task AddAsync(Module post);

        void Update(Module post);

        void Remove(Module tag);

        Task<PageResult<Module>> GetPaginatedPostsAsync(int page, int pageSize, string? search, string orderBy);

        Task<List<Module>> GetByModuleNames(IEnumerable<string> names);

        #endregion Public Methods
    }
}