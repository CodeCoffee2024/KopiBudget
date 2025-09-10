using KopiBudget.Domain.Entities;
using KopiBudget.Domain.Interfaces;
using KopiBudget.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace KopiBudget.Infrastructure.Repositories
{
    public class SystemSettingsRepository : RepositoryBase<SystemSettings>, ISystemSettingsRepository
    {
        #region Public Constructors

        public SystemSettingsRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<SystemSettings?> GetSettingsAsync()
        {
            return await _context.Set<SystemSettings>().FirstOrDefaultAsync();
        }

        #endregion Public Constructors
    }
}