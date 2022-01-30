using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ipme.WikiBeer.Persistance.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BeerColor",
                columns: table => new
                {
                    ColorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ColorId", x => x.ColorId);
                });

            migrationBuilder.CreateTable(
                name: "BeerStyle",
                columns: table => new
                {
                    StyleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("StyleId", x => x.StyleId);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CountryId", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "Ingredient",
                columns: table => new
                {
                    IngredientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Use = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    Ebc = table.Column<float>(type: "real", nullable: true),
                    AlphaAcid = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("IngredientId", x => x.IngredientId);
                });

            migrationBuilder.CreateTable(
                name: "Brewery",
                columns: table => new
                {
                    BreweryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("BreweryId", x => x.BreweryId);
                    table.ForeignKey(
                        name: "FK_Brewery_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Beer",
                columns: table => new
                {
                    BeerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    Ibu = table.Column<float>(type: "real", nullable: true),
                    Degree = table.Column<float>(type: "real", nullable: false),
                    BreweryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StyleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ColorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("BeerId", x => x.BeerId);
                    table.ForeignKey(
                        name: "FK_Beer_BeerColor_ColorId",
                        column: x => x.ColorId,
                        principalTable: "BeerColor",
                        principalColumn: "ColorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Beer_BeerStyle_StyleId",
                        column: x => x.StyleId,
                        principalTable: "BeerStyle",
                        principalColumn: "StyleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Beer_Brewery_BreweryId",
                        column: x => x.BreweryId,
                        principalTable: "Brewery",
                        principalColumn: "BreweryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BeerIngredient",
                columns: table => new
                {
                    BeersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IngredientsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeerIngredient", x => new { x.BeersId, x.IngredientsId });
                    table.ForeignKey(
                        name: "FK_BeerIngredient_Beer_BeersId",
                        column: x => x.BeersId,
                        principalTable: "Beer",
                        principalColumn: "BeerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BeerIngredient_Ingredient_IngredientsId",
                        column: x => x.IngredientsId,
                        principalTable: "Ingredient",
                        principalColumn: "IngredientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Beer_BreweryId",
                table: "Beer",
                column: "BreweryId");

            migrationBuilder.CreateIndex(
                name: "IX_Beer_ColorId",
                table: "Beer",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Beer_StyleId",
                table: "Beer",
                column: "StyleId");

            migrationBuilder.CreateIndex(
                name: "IX_BeerIngredient_IngredientsId",
                table: "BeerIngredient",
                column: "IngredientsId");

            migrationBuilder.CreateIndex(
                name: "IX_Brewery_CountryId",
                table: "Brewery",
                column: "CountryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BeerIngredient");

            migrationBuilder.DropTable(
                name: "Beer");

            migrationBuilder.DropTable(
                name: "Ingredient");

            migrationBuilder.DropTable(
                name: "BeerColor");

            migrationBuilder.DropTable(
                name: "BeerStyle");

            migrationBuilder.DropTable(
                name: "Brewery");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}
