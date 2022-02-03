using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ipme.WikiBeer.Persistance.Migrations
{
    public partial class tt15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beer_BeerStyle_BeerStyleEntityId",
                table: "Beer");

            migrationBuilder.DropIndex(
                name: "IX_Beer_BeerStyleEntityId",
                table: "Beer");

            migrationBuilder.DropColumn(
                name: "BeerStyleEntityId",
                table: "Beer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "FK_Beer_BeerStyle_BeerStyleEntityId",
                table: "Beer",
                column: "BeerStyleEntityId",
                principalTable: "BeerStyle",
                principalColumn: "StyleId");
        }
    }
}
