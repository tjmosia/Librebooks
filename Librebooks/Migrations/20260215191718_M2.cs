using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Librebooks.Migrations
{
	/// <inheritdoc />
	public partial class M2 : Migration
	{
		/// <inheritdoc />
		protected override void Up (MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "Birthday",
				table: "User");
		}

		/// <inheritdoc />
		protected override void Down (MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<DateOnly>(
				name: "Birthday",
				table: "User",
				type: "TEXT",
				nullable: true);
		}
	}
}
