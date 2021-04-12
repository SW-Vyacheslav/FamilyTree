using Microsoft.EntityFrameworkCore.Migrations;

namespace FamilyTree.Infrastructure.Migrations
{
    public partial class NamesFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryType",
                table: "DataCategories");

            migrationBuilder.AddColumn<int>(
                name: "DataCategoryType",
                table: "DataCategories",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataCategoryType",
                table: "DataCategories");

            migrationBuilder.AddColumn<int>(
                name: "CategoryType",
                table: "DataCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
