using KopiBudget.Application.Interfaces.Common;
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
            //services.AddScoped<IFileService, FileService>();

            // --- Repositories ---
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }

        #endregion Public Methods

        #region Private Methods

        private static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
        {
            var connectionString = "Host=db.hjsepjjcvdiypqjieseh.supabase.co;Port=5432;Database=postgres;Username=postgres;Password=Mn#m0n1c5123;Trust Server Certificate=true;Include Error Detail=true;SSL Mode=Require";

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
