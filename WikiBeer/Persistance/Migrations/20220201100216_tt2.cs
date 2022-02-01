using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ipme.WikiBeer.Persistance.Migrations
{
    public partial class tt2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ALACON",
                table: "Beer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "tttt");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ALACON",
                table: "Beer");
        }
    }
}
