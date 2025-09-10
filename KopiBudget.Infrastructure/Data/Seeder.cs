using KopiBudget.Application.Interfaces.Common;
using KopiBudget.Domain.Entities;
using KopiBudget.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace KopiBudget.Infrastructure.Data
{
    public static class Seeder
    {
        #region Public Methods

        public static async Task SeedAsync(
            AppDbContext context,
            ILogger logger,
            IConfiguration configuration,
            IPasswordHasherService passwordHasherService,
            IUserRepository userRepository)
        {
            await context.Database.MigrateAsync();

            // --- Seed default admin user ---
            var admin = await userRepository.GetByUsernameAsync("admin");
            if (admin is null)
            {
                var user = User.Register(
                    "admin",
                    "admin@email.com",
                    passwordHasherService.HashPassword("password"),
                    "admin",
                    "admin",
                    ""
                );
                user.FlagAsSystemGenerated();
                context.Users.Add(user);
                await context.SaveChangesAsync();

                logger.LogInformation("Seeded default user.");
                admin = user;
            }

            if (!context.Modules.Any())
            {
                var modules = new[]
                {
                    Module.Create("Modules", "/modules", admin.Id!.Value),
                    Module.Create("Categories", "/categories", admin.Id!.Value),
                    Module.Create("Accounts", "/accounts", admin.Id!.Value),
                    Module.Create("Transactions", "/transactions", admin.Id!.Value),
                    Module.Create("Budgets", "/budgets", admin.Id!.Value),
                    Module.Create("Users", "/users", admin.Id!.Value),
                    Module.Create("Dashboard", "/admin/dashboard", admin.Id!.Value)
                };

                foreach (var module in modules)
                    module.FlagAsSystemGenerated();

                context.Modules.AddRange(modules);
                await context.SaveChangesAsync();

                logger.LogInformation("Seeded modules.");

                foreach (var module in modules)
                {
                    if (!module.Permissions.Any())
                    {
                        var viewPermission = module.AddPermission("View", module.Id);
                        var editPermission = module.AddPermission("Modify", module.Id);

                        context.Permissions.AddRange(viewPermission, editPermission);
                        await context.SaveChangesAsync();

                        logger.LogInformation("Seeded permissions for all modules.");
                    }
                }
            }
            if (!context.Roles.Any())
            {
                var adminRole = Role.Create("Admin", admin!.Id!.Value);
                var userRole = Role.Create("User", admin!.Id!.Value);

                adminRole.FlagAsSystemGenerated();
                userRole.FlagAsSystemGenerated();

                context.Roles.AddRange(adminRole, userRole);
                await context.SaveChangesAsync();

                logger.LogInformation("Seeded roles.");

                var allPermissions = await context.Permissions.ToListAsync();

                foreach (var permission in allPermissions)
                {
                    adminRole.AddPermission(permission!);
                    await context.SaveChangesAsync();
                }

                var userPermissions = await context.Permissions.Where(it =>
                it.Module!.Name == "Dashboard" ||
                it.Module!.Name == "Budgets" ||
                it.Module!.Name == "Categories" ||
                it.Module!.Name == "Transactions" ||
                it.Module!.Name == "Accounts").ToListAsync();
                foreach (var permission in userPermissions)
                {
                    userRole.AddPermission(permission!);
                    await context.SaveChangesAsync();
                }

                await context.SaveChangesAsync();
                logger.LogInformation("Assigned all permissions to Admin role.");

                admin.AssignRole(adminRole);
                await context.SaveChangesAsync();

                logger.LogInformation("Assigned admin user to Admin role.");
            }
            if (!context.SystemSettings.Any())
            {
                var entity = SystemSettings.Create(configuration["ExchangeRateApi:DefaultCurrency"]!, admin!.Id!.Value);
                context.SystemSettings.Add(entity);
                await context.SaveChangesAsync();
            }
        }

        #endregion Public Methods
    }
}