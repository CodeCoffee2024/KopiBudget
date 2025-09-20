using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KopiBudget.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Alter_Category_Add_Table_Budget_Personal_Category : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_Categories_CategoryId",
                schema: "public",
                table: "Budgets");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                schema: "public",
                table: "Budgets",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateTable(
                name: "PersonalCategories",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Icon = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Color = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedById = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonalCategories_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalSchema: "public",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PersonalCategories_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalSchema: "public",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BudgetPersonalCategories",
                schema: "public",
                columns: table => new
                {
                    PersonalCategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    BudgetId = table.Column<Guid>(type: "uuid", nullable: false),
                    Limit = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetPersonalCategories", x => new { x.BudgetId, x.PersonalCategoryId });
                    table.ForeignKey(
                        name: "FK_BudgetPersonalCategories_Budgets_BudgetId",
                        column: x => x.BudgetId,
                        principalSchema: "public",
                        principalTable: "Budgets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BudgetPersonalCategories_PersonalCategories_PersonalCategor~",
                        column: x => x.PersonalCategoryId,
                        principalSchema: "public",
                        principalTable: "PersonalCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BudgetPersonalCategories_PersonalCategoryId",
                schema: "public",
                table: "BudgetPersonalCategories",
                column: "PersonalCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalCategories_CreatedById",
                schema: "public",
                table: "PersonalCategories",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalCategories_UpdatedById",
                schema: "public",
                table: "PersonalCategories",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_Categories_CategoryId",
                schema: "public",
                table: "Budgets",
                column: "CategoryId",
                principalSchema: "public",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_Categories_CategoryId",
                schema: "public",
                table: "Budgets");

            migrationBuilder.DropTable(
                name: "BudgetPersonalCategories",
                schema: "public");

            migrationBuilder.DropTable(
                name: "PersonalCategories",
                schema: "public");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                schema: "public",
                table: "Budgets",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_Categories_CategoryId",
                schema: "public",
                table: "Budgets",
                column: "CategoryId",
                principalSchema: "public",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
