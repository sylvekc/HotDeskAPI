using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotDeskAPI.Migrations
{
    public partial class AddFix2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeskLocation",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DeskNumber",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeskLocation",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "DeskNumber",
                table: "Reservations");
        }
    }
}
