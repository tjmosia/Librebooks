using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Librebooks.Migrations
{
    /// <inheritdoc />
    public partial class M4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UniversalId",
                table: "Company",
                type: "TEXT",
                maxLength: 155,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UniversalId",
                table: "Company");
        }
    }
}
