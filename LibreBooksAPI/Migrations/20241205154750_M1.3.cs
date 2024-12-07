using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibreBooksAPI.Migrations
{
    /// <inheritdoc />
    public partial class M13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Logo",
                table: "Company",
                newName: "PhysicalAddress");

            migrationBuilder.RenameColumn(
                name: "DeliveryAddress",
                table: "Company",
                newName: "LogoId");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Photo",
                table: "User",
                type: "BLOB",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "CompanyLogo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Logo = table.Column<byte[]>(type: "BLOB", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CompanyId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyLogo", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Company_LogoId",
                table: "Company",
                column: "LogoId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Company_CompanyLogo_LogoId",
                table: "Company",
                column: "LogoId",
                principalTable: "CompanyLogo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Company_CompanyLogo_LogoId",
                table: "Company");

            migrationBuilder.DropTable(
                name: "CompanyLogo");

            migrationBuilder.DropIndex(
                name: "IX_Company_LogoId",
                table: "Company");

            migrationBuilder.RenameColumn(
                name: "PhysicalAddress",
                table: "Company",
                newName: "Logo");

            migrationBuilder.RenameColumn(
                name: "LogoId",
                table: "Company",
                newName: "DeliveryAddress");

            migrationBuilder.AlterColumn<string>(
                name: "Photo",
                table: "User",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "BLOB",
                oldNullable: true);
        }
    }
}
