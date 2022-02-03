using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ipme.WikiBeer.Persistance.Migrations
{
    public partial class tt13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beer_BeerStyle_StyleId",
                table: "Beer");

            migrationBuilder.DropForeignKey(
                name: "FK_Beer_BeerStyle_StyleId1",
                table: "Beer");

            migrationBuilder.DropIndex(
                name: "IX_Beer_BeerStyleEntityId",
                table: "Beer");

            migrationBuilder.DropColumn(
                name: "BeerStyleEntityId",
                table: "Beer");

            migrationBuilder.AddForeignKey(
                name: "FK_Beer_BeerStyle_StyleId",
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

            migrationBuilder.AddColumn<Guid>(
                name: "BeerStyleEntityId",
                table: "Beer",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Beer_BeerStyleEntityId",
                table: "Beer",
                column: "BeerStyleEntityId");

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
    }
}
