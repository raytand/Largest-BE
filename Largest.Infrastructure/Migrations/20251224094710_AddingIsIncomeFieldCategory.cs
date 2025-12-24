using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Largest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingIsIncomeFieldCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsIncome",
                table: "Categories",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsIncome",
                table: "Categories");
        }
    }
}
