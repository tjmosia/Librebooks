using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Librebooks.Migrations
{
    /// <inheritdoc />
    public partial class M3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VerificationRequest",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Subject = table.Column<string>(type: "TEXT", nullable: true),
                    RequestUrl = table.Column<string>(type: "TEXT", nullable: true),
                    HashString = table.Column<string>(type: "TEXT", nullable: true),
                    Verified = table.Column<bool>(type: "INTEGER", nullable: false),
                    ValidUntil = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Attempts = table.Column<short>(type: "INTEGER", nullable: false),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerificationRequest", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VerificationRequest");
        }
    }
}
