using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Largest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SharingBalances : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Balances_Users_UserId",
                table: "Balances");

            migrationBuilder.DropIndex(
                name: "IX_Balances_UserId",
                table: "Balances");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Balances");

            migrationBuilder.AlterColumn<int>(
                name: "BalanceId",
                table: "Transactions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "BalanceUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BalanceId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    Role = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BalanceUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BalanceUsers_Balances_BalanceId",
                        column: x => x.BalanceId,
                        principalTable: "Balances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BalanceUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BalanceUsers_BalanceId_UserId",
                table: "BalanceUsers",
                columns: new[] { "BalanceId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BalanceUsers_UserId",
                table: "BalanceUsers",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BalanceUsers");

            migrationBuilder.AlterColumn<int>(
                name: "BalanceId",
                table: "Transactions",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Balances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Balances_UserId",
                table: "Balances",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Balances_Users_UserId",
                table: "Balances",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
