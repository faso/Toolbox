using Microsoft.EntityFrameworkCore.Migrations;

namespace Toolbox.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Widgets",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Widgets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserWidgets",
                columns: table => new
                {
                    WidgetId = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWidgets", x => new { x.WidgetId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserWidgets_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserWidgets_Widgets_WidgetId",
                        column: x => x.WidgetId,
                        principalTable: "Widgets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserWidgets_UserId",
                table: "UserWidgets",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserWidgets");

            migrationBuilder.DropTable(
                name: "Widgets");
        }
    }
}
