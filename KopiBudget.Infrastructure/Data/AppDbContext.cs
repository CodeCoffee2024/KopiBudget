using KopiBudget.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace KopiBudget.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        #region Public Constructors

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        #endregion Public Constructors

        #region Properties

        public DbSet<User> Users => Set<User>();
        public DbSet<Account> Accounts => Set<Account>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<KopiBudget.Domain.Entities.Transaction> Transactions => Set<KopiBudget.Domain.Entities.Transaction>();
        public DbSet<Budget> Budgets => Set<Budget>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<KopiBudget.Domain.Entities.Module> Modules => Set<KopiBudget.Domain.Entities.Module>();
        public DbSet<Permission> Permissions => Set<Permission>();
        public DbSet<UserRole> UserRoles => Set<UserRole>();
        public DbSet<RolePermission> RolePermissions => Set<RolePermission>();

        #endregion Properties

        #region Protected Methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        #endregion Protected Methods
    }
}