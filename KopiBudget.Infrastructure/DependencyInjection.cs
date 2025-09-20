using KopiBudget.Application.Interfaces.Common;
using KopiBudget.Application.Interfaces.Services;
using KopiBudget.Domain.Interfaces;
using KopiBudget.Infrastructure.Data;
using KopiBudget.Infrastructure.Repositories;
using KopiBudget.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KopiBudget.Infrastructure
{
    public static class DependencyInjection
    {
        #region Public Methods

        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // --- Persistence ---
            services.AddPersistence(configuration);

            // --- Core Services ---
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IPasswordHasherService, PasswordHasherService>();
            services.AddScoped<ISystemSettingsService, SystemSettingsService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddSingleton<IExchangeRateProviderService, ExchangeRateService>();
            services.AddHostedService(sp => (ExchangeRateService)sp.GetRequiredService<IExchangeRateProviderService>());

            // --- Repositories ---
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IModuleRepository, ModuleRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ISystemSettingsRepository, SystemSettingsRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IPersonalCategoryRepository, PersonalCategoryRepository>();
            services.AddScoped<IBudgetRepository, BudgetRepository>();
            services.AddScoped<IBudgetPersonalCategoryRepository, BudgetPersonalCategoryRepository>();

            return services;
        }

        #endregion Public Methods

        #region Private Methods

        private static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(configuration), "Database connection string is missing!");

            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connectionString)
                       .UseLazyLoadingProxies());

            return services;
        }

        #endregion Private Methods
    }
}