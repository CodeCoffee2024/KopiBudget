using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KopiBudget.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Alter_Category_Account_Move_IsExpense : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Users_UserId",
                schema: "public",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "IsExpense",
                schema: "public",
                table: "Categories");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                schema: "public",
                table: "Categories",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                schema: "public",
                table: "Accounts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsExpense",
                schema: "public",
                table: "Accounts",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CategoryId",
                schema: "public",
                table: "Accounts",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Categories_CategoryId",
                schema: "public",
                table: "Accounts",
                column: "CategoryId",
                principalSchema: "public",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Users_UserId",
                schema: "public",
                table: "Categories",
                column: "UserId",
                principalSchema: "public",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Categories_CategoryId",
                schema: "public",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Users_UserId",
                schema: "public",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_CategoryId",
                schema: "public",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                schema: "public",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "IsExpense",
                schema: "public",
                table: "Accounts");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                schema: "public",
                table: "Categories",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsExpense",
                schema: "public",
                table: "Categories",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Users_UserId",
                schema: "public",
                table: "Categories",
                column: "UserId",
                principalSchema: "public",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
