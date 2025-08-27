using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KopiBudget.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PendingChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Users",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "UserRoles",
                newName: "UserRoles",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "Transactions",
                newName: "Transactions",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "Roles",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "RolePermissions",
                newName: "RolePermissions",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "Permissions",
                newName: "Permissions",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "Modules",
                newName: "Modules",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Categories",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "Budgets",
                newName: "Budgets",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "Accounts",
                newName: "Accounts",
                newSchema: "public");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Users",
                schema: "public",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "UserRoles",
                schema: "public",
                newName: "UserRoles");

            migrationBuilder.RenameTable(
                name: "Transactions",
                schema: "public",
                newName: "Transactions");

            migrationBuilder.RenameTable(
                name: "Roles",
                schema: "public",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "RolePermissions",
                schema: "public",
                newName: "RolePermissions");

            migrationBuilder.RenameTable(
                name: "Permissions",
                schema: "public",
                newName: "Permissions");

            migrationBuilder.RenameTable(
                name: "Modules",
                schema: "public",
                newName: "Modules");

            migrationBuilder.RenameTable(
                name: "Categories",
                schema: "public",
                newName: "Categories");

            migrationBuilder.RenameTable(
                name: "Budgets",
                schema: "public",
                newName: "Budgets");

            migrationBuilder.RenameTable(
                name: "Accounts",
                schema: "public",
                newName: "Accounts");
        }
    }
}
