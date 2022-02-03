using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ipme.WikiBeer.Persistance.Migrations
{
    public partial class tt12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beer_BeerStyle_BeerStyleEntityId",
                table: "Beer");

            migrationBuilder.DropForeignKey(
                name: "FK_Beer_BeerStyle_StyleId",
                table: "Beer");

            migrationBuilder.AddForeignKey(
                name: "FK_Beer_BeerStyle_StyleId",
                table: "Beer",
                column: "BeerStyleEntityId",
                principalTable: "BeerStyle",
                principalColumn: "StyleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Beer_BeerStyle_StyleId1",
                table: "Beer",
                column: "StyleId",
                principalTable: "BeerStyle",
                principalColumn: "StyleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beer_BeerStyle_StyleId",
                table: "Beer");

            migrationBuilder.DropForeignKey(
                name: "FK_Beer_BeerStyle_StyleId1",
                table: "Beer");

            migrationBuilder.AddForeignKey(
                name: "FK_Beer_BeerStyle_BeerStyleEntityId",
                table: "Beer",
                column: "BeerStyleEntityId",
                principalTable: "BeerStyle",
                principalColumn: "StyleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Beer_BeerStyle_StyleId",
                table: "Beer",
                column: "StyleId",
                principalTable: "BeerStyle",
                principalColumn: "StyleId");
        }
    }
}
