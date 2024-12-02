using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibreBooksAPI.Migrations
{
    /// <inheritdoc />
    public partial class M11 : Migration
    {
        /// <inheritdoc />
        protected override void Up (MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValueAddedTaxNumber",
                table: "Company",
                newName: "VATNumber");

            migrationBuilder.RenameColumn(
                name: "Telephone",
                table: "Company",
                newName: "TelephoneNumber");

            migrationBuilder.AddColumn<string>(
                name: "MobileNumber",
                table: "Company",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down (MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MobileNumber",
                table: "Company");

            migrationBuilder.RenameColumn(
                name: "VATNumber",
                table: "Company",
                newName: "ValueAddedTaxNumber");

            migrationBuilder.RenameColumn(
                name: "TelephoneNumber",
                table: "Company",
                newName: "Telephone");
        }
    }
}
