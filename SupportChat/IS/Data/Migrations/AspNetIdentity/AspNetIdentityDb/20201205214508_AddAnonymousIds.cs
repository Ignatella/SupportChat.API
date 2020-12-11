using Microsoft.EntityFrameworkCore.Migrations;

namespace IS.Data.Migrations.AspNetIdentity.AspNetIdentityDb
{
    public partial class AddAnonymousIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUserAnonymousId",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    AnonymousId = table.Column<string>(type: "text", nullable: true),
                    AppUserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserAnonymousId", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUserAnonymousId_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserAnonymousId_AppUserId",
                table: "AppUserAnonymousId",
                column: "AppUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserAnonymousId");
        }
    }
}
