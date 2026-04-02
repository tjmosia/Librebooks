using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Librebooks.Migrations
{
    /// <inheritdoc />
    public partial class M2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tax_Name",
                table: "Tax");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Tax_Name",
                table: "Tax",
                column: "Name",
                unique: true);
        }
    }
}
