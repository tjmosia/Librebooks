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
            migrationBuilder.AlterColumn<string>(
                name: "RowVersion",
                table: "Currency",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RowVersion",
                table: "BusinessSector",
                type: "TEXT",
                rowVersion: true,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldRowVersion: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RowVersion",
                table: "Currency",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "RowVersion",
                table: "BusinessSector",
                type: "TEXT",
                rowVersion: true,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldRowVersion: true,
                oldNullable: true);
        }
    }
}
