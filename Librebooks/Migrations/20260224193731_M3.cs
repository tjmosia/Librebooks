using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Librebooks.Migrations;

/// <inheritdoc />
public partial class M3 : Migration
{
	/// <inheritdoc />
	protected override void Up (MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AddColumn<string>(
			name: "AssociatedTo",
			table: "UserRole",
			type: "TEXT",
			maxLength: 100,
			nullable: true);
	}

	/// <inheritdoc />
	protected override void Down (MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropColumn(
			name: "AssociatedTo",
			table: "UserRole");
	}
}
