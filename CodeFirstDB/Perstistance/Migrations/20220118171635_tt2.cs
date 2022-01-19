using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Perstistance.Migrations
{
    public partial class tt2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IngredientId",
                table: "Ingredient",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Ingredient",
                newName: "IngredientId");
        }
    }
}
