using KopiBudget.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace KopiBudget.Api.Data
{
    public class AppDbContext : DbContext
    {
        #region Public Constructors

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        #endregion Public Constructors

        #region Properties

        public DbSet<FinanceTransaction> Transactions { get; set; }

        #endregion Properties
    }
}