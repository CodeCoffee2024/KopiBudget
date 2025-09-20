using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KopiBudget.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Alter_Budget_Personal_Category_Add_Transaction_Amount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TransactionAmount",
                schema: "public",
                table: "BudgetPersonalCategories",
                type: "numeric",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionAmount",
                schema: "public",
                table: "BudgetPersonalCategories");
        }
    }
}
