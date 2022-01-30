using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ipme.WikiBeer.Persistance.Migrations
{
    public partial class tt2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Degree",
                table: "Beer");

            migrationBuilder.DropColumn(
                name: "Ibu",
                table: "Beer");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Beer",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Beer",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<float>(
                name: "Degree",
                table: "Beer",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Ibu",
                table: "Beer",
                type: "real",
                nullable: true);
        }
    }
}
