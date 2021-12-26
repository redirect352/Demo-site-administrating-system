using Microsoft.EntityFrameworkCore.Migrations;

namespace Task4.Migrations
{
    public partial class AddedStatusId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Statuses_StatusId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Statuses_StatusId",
                table: "AspNetUsers",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Statuses_StatusId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Statuses_StatusId",
                table: "AspNetUsers",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
