using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KopiBudget.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Alter_Transaction_Set_BudgetId_Cascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Budgets_BudgetId",
                schema: "public",
                table: "Transactions");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Budgets_BudgetId",
                schema: "public",
                table: "Transactions",
                column: "BudgetId",
                principalSchema: "public",
                principalTable: "Budgets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Budgets_BudgetId",
                schema: "public",
                table: "Transactions");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Budgets_BudgetId",
                schema: "public",
                table: "Transactions",
                column: "BudgetId",
                principalSchema: "public",
                principalTable: "Budgets",
                principalColumn: "Id");
        }
    }
}
