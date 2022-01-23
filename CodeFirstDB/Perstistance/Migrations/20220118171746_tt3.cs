using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Perstistance.Migrations
{
    public partial class tt3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BreweryId",
                table: "Brewery",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Brewery",
                newName: "BreweryId");
        }
    }
}
