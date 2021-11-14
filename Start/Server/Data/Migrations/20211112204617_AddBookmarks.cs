using Microsoft.EntityFrameworkCore.Migrations;

namespace Start.Server.Data.Migrations
{
    public partial class AddBookmarks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookmarkContainers",
                columns: table => new
                {
                    BookmarkContainerId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    ApplicationUserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookmarkContainers", x => x.BookmarkContainerId);
                    table.ForeignKey(
                        name: "FK_BookmarkContainers_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookmarkGroups",
                columns: table => new
                {
                    BookmarkGroupId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    Color = table.Column<string>(type: "TEXT", maxLength: 6, nullable: false),
                    BookmarkContainerId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookmarkGroups", x => x.BookmarkGroupId);
                    table.ForeignKey(
                        name: "FK_BookmarkGroups_BookmarkContainers_BookmarkContainerId",
                        column: x => x.BookmarkContainerId,
                        principalTable: "BookmarkContainers",
                        principalColumn: "BookmarkContainerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookmarks",
                columns: table => new
                {
                    BookmarkId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    Url = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 5000, nullable: true),
                    BookmarkGroupId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookmarks", x => x.BookmarkId);
                    table.ForeignKey(
                        name: "FK_Bookmarks_BookmarkGroups_BookmarkGroupId",
                        column: x => x.BookmarkGroupId,
                        principalTable: "BookmarkGroups",
                        principalColumn: "BookmarkGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookmarkContainers_ApplicationUserId",
                table: "BookmarkContainers",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BookmarkGroups_BookmarkContainerId",
                table: "BookmarkGroups",
                column: "BookmarkContainerId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookmarks_BookmarkGroupId",
                table: "Bookmarks",
                column: "BookmarkGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookmarks");

            migrationBuilder.DropTable(
                name: "BookmarkGroups");

            migrationBuilder.DropTable(
                name: "BookmarkContainers");
        }
    }
}
