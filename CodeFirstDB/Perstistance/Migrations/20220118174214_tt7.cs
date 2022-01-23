using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Perstistance.Migrations
{
    public partial class tt7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beer_BeerColor_ColorID",
                table: "Beer");

            migrationBuilder.RenameColumn(
                name: "ColorID",
                table: "BeerColor",
                newName: "ColorId");

            migrationBuilder.RenameColumn(
                name: "ColorID",
                table: "Beer",
                newName: "ColorId");

            migrationBuilder.RenameIndex(
                name: "IX_Beer_ColorID",
                table: "Beer",
                newName: "IX_Beer_ColorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Beer_BeerColor_ColorId",
                table: "Beer",
                column: "ColorId",
                principalTable: "BeerColor",
                principalColumn: "ColorId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beer_BeerColor_ColorId",
                table: "Beer");

            migrationBuilder.RenameColumn(
                name: "ColorId",
                table: "BeerColor",
                newName: "ColorID");

            migrationBuilder.RenameColumn(
                name: "ColorId",
                table: "Beer",
                newName: "ColorID");

            migrationBuilder.RenameIndex(
                name: "IX_Beer_ColorId",
                table: "Beer",
                newName: "IX_Beer_ColorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Beer_BeerColor_ColorID",
                table: "Beer",
                column: "ColorID",
                principalTable: "BeerColor",
                principalColumn: "ColorID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
