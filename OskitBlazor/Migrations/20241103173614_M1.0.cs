using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OskitBlazor.Migrations
{
    /// <inheritdoc />
    public partial class M10 : Migration
    {
        /// <inheritdoc />
        protected override void Up (MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BusinessSectorId",
                table: "Company",
                type: "nvarchar(450)",
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "YearsInBusienss",
                table: "Company",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BusinessSector",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessSector", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Company_BusinessSectorId",
                table: "Company",
                column: "BusinessSectorId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessSector_Name",
                table: "BusinessSector",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Company_BusinessSector_BusinessSectorId",
                table: "Company",
                column: "BusinessSectorId",
                principalTable: "BusinessSector",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down (MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Company_BusinessSector_BusinessSectorId",
                table: "Company");

            migrationBuilder.DropTable(
                name: "BusinessSector");

            migrationBuilder.DropIndex(
                name: "IX_Company_BusinessSectorId",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "BusinessSectorId",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "YearsInBusienss",
                table: "Company");
        }
    }
}
