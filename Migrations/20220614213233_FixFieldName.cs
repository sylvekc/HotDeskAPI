using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotDeskAPI.Migrations
{
    public partial class FixFieldName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Desks_Locations_LocationID",
                table: "Desks");

            migrationBuilder.RenameColumn(
                name: "LocationID",
                table: "Desks",
                newName: "LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Desks_LocationID",
                table: "Desks",
                newName: "IX_Desks_LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Desks_Locations_LocationId",
                table: "Desks",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Desks_Locations_LocationId",
                table: "Desks");

            migrationBuilder.RenameColumn(
                name: "LocationId",
                table: "Desks",
                newName: "LocationID");

            migrationBuilder.RenameIndex(
                name: "IX_Desks_LocationId",
                table: "Desks",
                newName: "IX_Desks_LocationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Desks_Locations_LocationID",
                table: "Desks",
                column: "LocationID",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
