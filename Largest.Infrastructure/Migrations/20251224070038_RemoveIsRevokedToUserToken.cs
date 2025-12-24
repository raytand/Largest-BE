using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Largest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveIsRevokedToUserToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RevokedTokens");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RevokedTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Expiration = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Token = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RevokedTokens", x => x.Id);
                });
        }
    }
}
