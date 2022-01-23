using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Perstistance.Migrations
{
    public partial class tt6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Beer",
                newName: "BeerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BeerId",
                table: "Beer",
                newName: "Id");
        }
    }
}
