using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ipme.WikiBeer.Persistance.Migrations
{
    public partial class tt8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beer_BeerColor_ColorId",
                table: "Beer");

            migrationBuilder.DropForeignKey(
                name: "FK_Beer_BeerStyle_StyleId",
                table: "Beer");

            migrationBuilder.DropForeignKey(
                name: "FK_Beer_Brewery_BreweryId",
                table: "Beer");

            migrationBuilder.AlterColumn<Guid>(
                name: "StyleId",
                table: "Beer",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "ColorId",
                table: "Beer",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "BreweryId",
                table: "Beer",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Beer_BeerColor_ColorId",
                table: "Beer",
                column: "ColorId",
                principalTable: "BeerColor",
                principalColumn: "ColorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Beer_BeerStyle_StyleId",
                table: "Beer",
                column: "StyleId",
                principalTable: "BeerStyle",
                principalColumn: "StyleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Beer_Brewery_BreweryId",
                table: "Beer",
                column: "BreweryId",
                principalTable: "Brewery",
                principalColumn: "BreweryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beer_BeerColor_ColorId",
                table: "Beer");

            migrationBuilder.DropForeignKey(
                name: "FK_Beer_BeerStyle_StyleId",
                table: "Beer");

            migrationBuilder.DropForeignKey(
                name: "FK_Beer_Brewery_BreweryId",
                table: "Beer");

            migrationBuilder.AlterColumn<Guid>(
                name: "StyleId",
                table: "Beer",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ColorId",
                table: "Beer",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "BreweryId",
                table: "Beer",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Beer_BeerColor_ColorId",
                table: "Beer",
                column: "ColorId",
                principalTable: "BeerColor",
                principalColumn: "ColorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Beer_BeerStyle_StyleId",
                table: "Beer",
                column: "StyleId",
                principalTable: "BeerStyle",
                principalColumn: "StyleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Beer_Brewery_BreweryId",
                table: "Beer",
                column: "BreweryId",
                principalTable: "Brewery",
                principalColumn: "BreweryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
