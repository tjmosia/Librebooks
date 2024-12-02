using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibreBooksAPI.Migrations
{
    /// <inheritdoc />
    public partial class M12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MobileNumber",
                table: "Company");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MobileNumber",
                table: "Company",
                type: "TEXT",
                nullable: true);
        }
    }
}
