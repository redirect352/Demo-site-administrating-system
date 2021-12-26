using Microsoft.EntityFrameworkCore.Migrations;

namespace Task4.Migrations
{
    public partial class AddedStatusMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_StatusId",
                table: "AspNetUsers",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Statuses_StatusId",
                table: "AspNetUsers",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Statuses_StatusId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_StatusId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "AspNetUsers");
        }
    }
}
