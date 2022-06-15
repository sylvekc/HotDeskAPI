using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotDeskAPI.Migrations
{
    public partial class AddFix1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Buiding",
                table: "Locations",
                newName: "Building");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Building",
                table: "Locations",
                newName: "Buiding");
        }
    }
}
