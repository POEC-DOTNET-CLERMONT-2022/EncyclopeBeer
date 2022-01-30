using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ipme.WikiBeer.Persistance.Migrations
{
    public partial class tt3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Degree",
                table: "Beer",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Beer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Ibu",
                table: "Beer",
                type: "real",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Degree",
                table: "Beer");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Beer");

            migrationBuilder.DropColumn(
                name: "Ibu",
                table: "Beer");
        }
    }
}
