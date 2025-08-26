using KopiBudget.Application.Interfaces.Common;
using KopiBudget.Infrastructure.Data;

namespace KopiBudget.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Fields

        private readonly AppDbContext _context;

        #endregion Fields

        #region Public Constructors

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        #endregion Public Methods
    }
}