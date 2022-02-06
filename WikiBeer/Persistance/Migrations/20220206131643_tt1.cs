using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ipme.WikiBeer.Persistance.Migrations
{
    public partial class tt1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brewery_Country_CountryId",
                table: "Brewery");

            migrationBuilder.AlterColumn<Guid>(
                name: "CountryId",
                table: "Brewery",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Brewery_Country_CountryId",
                table: "Brewery",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "CountryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brewery_Country_CountryEntityId",
                table: "Brewery");

            migrationBuilder.DropForeignKey(
                name: "FK_Brewery_Country_CountryId",
                table: "Brewery");

            migrationBuilder.DropIndex(
                name: "IX_Brewery_CountryEntityId",
                table: "Brewery");

            migrationBuilder.DropColumn(
                name: "CountryEntityId",
                table: "Brewery");

            migrationBuilder.AlterColumn<Guid>(
                name: "CountryId",
                table: "Brewery",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Brewery_Country_CountryId",
                table: "Brewery",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
