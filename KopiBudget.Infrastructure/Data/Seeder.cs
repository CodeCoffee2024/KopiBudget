using KopiBudget.Application.Interfaces.Common;
using KopiBudget.Domain.Entities;
using KopiBudget.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KopiBudget.Infrastructure.Data
{
    public static class Seeder
    {
        #region Public Methods

        public static async Task SeedAsync(
            AppDbContext context,
            ILogger logger,
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
                admin = user; // assign back
            }

            // --- Seed modules ---
            if (!context.Modules.Any())
            {
                var modules = new[]
                {
                Module.Create("Modules", "/modules", admin.Id!.Value),
                Module.Create("Posts", "/posts", admin.Id!.Value),
                Module.Create("Categories", "/categories", admin.Id!.Value),
                Module.Create("Users", "/users", admin.Id!.Value),
                Module.Create("Dashboard", "/admin/dashboard", admin.Id!.Value)
            };

                foreach (var module in modules)
                    module.FlagAsSystemGenerated();

                context.Modules.AddRange(modules);
                await context.SaveChangesAsync();

                logger.LogInformation("Seeded modules.");

                // --- Seed permissions for Modules ---
                var modulesModule = modules.First(m => m.Name == "Modules");

                if (!modulesModule.Permissions.Any())
                {
                    var viewPermission = modulesModule.AddPermission("View", modulesModule.Id);
                    var editPermission = modulesModule.AddPermission("Modify", modulesModule.Id);

                    context.Permissions.AddRange(viewPermission, editPermission);
                    await context.SaveChangesAsync();

                    logger.LogInformation("Seeded permissions for Modules.");
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

                await context.SaveChangesAsync();
                logger.LogInformation("Assigned all permissions to Admin role.");

                admin.AssignRole(adminRole);
                await context.SaveChangesAsync();

                logger.LogInformation("Assigned admin user to Admin role.");
            }
        }

        #endregion Public Methods
    }
}