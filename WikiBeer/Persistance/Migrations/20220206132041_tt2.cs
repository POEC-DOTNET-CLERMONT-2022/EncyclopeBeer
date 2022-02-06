using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ipme.WikiBeer.Persistance.Migrations
{
    public partial class tt2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brewery_Country_CountryEntityId",
                table: "Brewery");

            migrationBuilder.DropIndex(
                name: "IX_Brewery_CountryEntityId",
                table: "Brewery");

            migrationBuilder.DropColumn(
                name: "CountryEntityId",
                table: "Brewery");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CountryEntityId",
                table: "Brewery",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Brewery_CountryEntityId",
                table: "Brewery",
                column: "CountryEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Brewery_Country_CountryEntityId",
                table: "Brewery",
                column: "CountryEntityId",
                principalTable: "Country",
                principalColumn: "CountryId");
        }
    }
}
