using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KopiBudget.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Alter_Table_Transactions_Add_Budget_Fields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_AccountId",
                schema: "public",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Categories_CategoryId",
                schema: "public",
                table: "Transactions");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                schema: "public",
                table: "Transactions",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "AccountId",
                schema: "public",
                table: "Transactions",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "BudgetId",
                schema: "public",
                table: "Transactions",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PersonalCategoryId",
                schema: "public",
                table: "Transactions",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_BudgetId",
                schema: "public",
                table: "Transactions",
                column: "BudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_PersonalCategoryId",
                schema: "public",
                table: "Transactions",
                column: "PersonalCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_AccountId",
                schema: "public",
                table: "Transactions",
                column: "AccountId",
                principalSchema: "public",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Budgets_BudgetId",
                schema: "public",
                table: "Transactions",
                column: "BudgetId",
                principalSchema: "public",
                principalTable: "Budgets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Categories_CategoryId",
                schema: "public",
                table: "Transactions",
                column: "CategoryId",
                principalSchema: "public",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_PersonalCategories_PersonalCategoryId",
                schema: "public",
                table: "Transactions",
                column: "PersonalCategoryId",
                principalSchema: "public",
                principalTable: "PersonalCategories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_AccountId",
                schema: "public",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Budgets_BudgetId",
                schema: "public",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Categories_CategoryId",
                schema: "public",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_PersonalCategories_PersonalCategoryId",
                schema: "public",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_BudgetId",
                schema: "public",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_PersonalCategoryId",
                schema: "public",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "BudgetId",
                schema: "public",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "PersonalCategoryId",
                schema: "public",
                table: "Transactions");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                schema: "public",
                table: "Transactions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AccountId",
                schema: "public",
                table: "Transactions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_AccountId",
                schema: "public",
                table: "Transactions",
                column: "AccountId",
                principalSchema: "public",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Categories_CategoryId",
                schema: "public",
                table: "Transactions",
                column: "CategoryId",
                principalSchema: "public",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
