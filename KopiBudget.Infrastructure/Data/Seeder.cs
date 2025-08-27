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

        public static async Task SeedAsync(AppDbContext context, ILogger logger, IPasswordHasherService passwordHasherService, IUserRepository userRepository)
        {
            await context.Database.MigrateAsync();

            // Seed data if none exists

            if (!context.Users.Any())
            {
                var user = User.Register("admin", "admin@email.com", passwordHasherService.HashPassword("password"), "admin", "admin", "");
                context.Users.Add(user);
                user.FlagAsSystemGenerated();
                await context.SaveChangesAsync();
                logger.LogInformation("Seeded default user.");
            }
            var admin = await userRepository.GetByUsernameAsync("admin");

            if (!context.Modules.Any())
            {
                // Create module
                Module[] modules = [
                    Module.Create("Modules", "/modules", admin.Id !.Value),
                        Module.Create("Posts", "/posts", admin.Id !.Value),
                        Module.Create("Categories", "/categories", admin.Id !.Value),
                        Module.Create("Users", "/users", admin.Id !.Value),
                        Module.Create("Dashboard", "/admin/dashboard", admin.Id !.Value)
                ];
                var module = modules[0];
                var post = modules[1];
                var category = modules[2];
                var user = modules[3];
                var dashboard = modules[4];
                dashboard.FlagAsSystemGenerated();
                // Add permissions module
                var viewPermissionModule = module.AddPermission("View");
                var editPermissionModule = module.AddPermission("Modify");
                module.FlagAsSystemGenerated();

                // Add permissions post
                var viewPermissionPost = post.AddPermission("View");
                var editPermissionPost = post.AddPermission("Modify");
                post.FlagAsSystemGenerated();

                // Add permissions category
                var viewPermissionCategory = category.AddPermission("View");
                var editPermissionCategory = category.AddPermission("Modify");
                category.FlagAsSystemGenerated();

                // Add permissions user
                var viewPermissionUser = user.AddPermission("View");
                var editPermissionUser = user.AddPermission("Modify");
                user.FlagAsSystemGenerated();

                // Create role
                var adminRole = Role.Create("Admin", admin.Id!.Value);
                adminRole.AddPermission(viewPermissionModule);
                adminRole.AddPermission(editPermissionModule);
                adminRole.AddPermission(viewPermissionPost);
                adminRole.AddPermission(editPermissionPost);
                adminRole.AddPermission(viewPermissionCategory);
                adminRole.AddPermission(editPermissionCategory);
                adminRole.AddPermission(viewPermissionUser);
                adminRole.AddPermission(editPermissionUser);
                admin.AssignRole(adminRole);

                // Add to DB
                await context.Modules.AddRangeAsync(modules);
                await context.Roles.AddAsync(adminRole);
                await context.SaveChangesAsync();
            }
            // Seed Categories
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    Category.Create("Food", true, admin.Id!.Value, DateTime.Now),
                    Category.Create("Transport", true, admin.Id!.Value, DateTime.Now),
                    Category.Create("Utilities", true, admin.Id!.Value, DateTime.Now),
                    Category.Create("Investment", true, admin.Id!.Value, DateTime.Now),
                    Category.Create("Salary", false, admin.Id!.Value, DateTime.Now)
                );

                await context.SaveChangesAsync();
            }

            // Seed Budgets
            if (!context.Budgets.Any())
            {
                var foodCategory = context.Categories.First(c => c.Name == "Food");
                var transportCategory = context.Categories.First(c => c.Name == "Transport");
                var entity1 = Budget.Create(10000, "Monthly Food Budget", DateTime.UtcNow, DateTime.UtcNow.AddMonths(1), foodCategory.Id!.Value!, null, null, null);
                var entity2 = Budget.Create(3000, "Transport Budget", DateTime.UtcNow, DateTime.UtcNow.AddMonths(1), foodCategory.Id!.Value!, null, null, null);
                context.Budgets.AddRange(entity1, entity2);
            }
            await context.SaveChangesAsync();
        }

        #endregion Public Methods
    }
}