using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameZone.DataBase.Migrations
{
    public partial class AddedImagesToGameModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryGame_Categories_MyPropertyId",
                table: "CategoryGame");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryGame",
                table: "CategoryGame");

            migrationBuilder.DropIndex(
                name: "IX_CategoryGame_MyPropertyId",
                table: "CategoryGame");

            migrationBuilder.RenameColumn(
                name: "MyPropertyId",
                table: "CategoryGame",
                newName: "CategoriesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryGame",
                table: "CategoryGame",
                columns: new[] { "CategoriesId", "GamesId" });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryGame_GamesId",
                table: "CategoryGame",
                column: "GamesId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_GameId",
                table: "Images",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryGame_Categories_CategoriesId",
                table: "CategoryGame",
                column: "CategoriesId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryGame_Categories_CategoriesId",
                table: "CategoryGame");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryGame",
                table: "CategoryGame");

            migrationBuilder.DropIndex(
                name: "IX_CategoryGame_GamesId",
                table: "CategoryGame");

            migrationBuilder.RenameColumn(
                name: "CategoriesId",
                table: "CategoryGame",
                newName: "MyPropertyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryGame",
                table: "CategoryGame",
                columns: new[] { "GamesId", "MyPropertyId" });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryGame_MyPropertyId",
                table: "CategoryGame",
                column: "MyPropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryGame_Categories_MyPropertyId",
                table: "CategoryGame",
                column: "MyPropertyId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
