using Microsoft.EntityFrameworkCore.Migrations;

namespace Start.Server.Data.Migrations {
	public partial class AddSorting : Migration {
		protected override void Up(MigrationBuilder migrationBuilder) {
			migrationBuilder.AddColumn<int>(
				name: "SortOrder",
				table: "Bookmarks",
				type: "INTEGER",
				nullable: false,
				defaultValue: 0);

			migrationBuilder.AddColumn<int>(
				name: "SortOrder",
				table: "BookmarkGroups",
				type: "INTEGER",
				nullable: false,
				defaultValue: 0);

			migrationBuilder.AddColumn<int>(
				name: "SortOrder",
				table: "BookmarkContainers",
				type: "INTEGER",
				nullable: false,
				defaultValue: 0);
		}

		protected override void Down(MigrationBuilder migrationBuilder) {
			migrationBuilder.DropColumn(
				name: "SortOrder",
				table: "Bookmarks");

			migrationBuilder.DropColumn(
				name: "SortOrder",
				table: "BookmarkGroups");

			migrationBuilder.DropColumn(
				name: "SortOrder",
				table: "BookmarkContainers");
		}
	}
}
