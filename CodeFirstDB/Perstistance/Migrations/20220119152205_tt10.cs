using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Perstistance.Migrations
{
    public partial class tt10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BeerIngredient_Ingredient_IngredientsId",
                table: "BeerIngredient");

            migrationBuilder.RenameColumn(
                name: "IngredientsId",
                table: "BeerIngredient",
                newName: "IngredientId");

            migrationBuilder.RenameIndex(
                name: "IX_BeerIngredient_IngredientsId",
                table: "BeerIngredient",
                newName: "IX_BeerIngredient_IngredientId");

            migrationBuilder.AddForeignKey(
                name: "FK_BeerIngredient_Ingredient_IngredientId",
                table: "BeerIngredient",
                column: "IngredientId",
                principalTable: "Ingredient",
                principalColumn: "IngredientId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BeerIngredient_Ingredient_IngredientId",
                table: "BeerIngredient");

            migrationBuilder.RenameColumn(
                name: "IngredientId",
                table: "BeerIngredient",
                newName: "IngredientsId");

            migrationBuilder.RenameIndex(
                name: "IX_BeerIngredient_IngredientId",
                table: "BeerIngredient",
                newName: "IX_BeerIngredient_IngredientsId");

            migrationBuilder.AddForeignKey(
                name: "FK_BeerIngredient_Ingredient_IngredientsId",
                table: "BeerIngredient",
                column: "IngredientsId",
                principalTable: "Ingredient",
                principalColumn: "IngredientId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
