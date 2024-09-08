using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameShopModel.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRelationGanre_GameProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameProducts_Genres_GenreId",
                table: "GameProducts");

            migrationBuilder.DropIndex(
                name: "IX_GameProducts_GenreId",
                table: "GameProducts");

            migrationBuilder.DropColumn(
                name: "GenreId",
                table: "GameProducts");

            migrationBuilder.CreateTable(
                name: "GameProductGenre",
                columns: table => new
                {
                    GameProductsId = table.Column<int>(type: "int", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameProductGenre", x => new { x.GameProductsId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_GameProductGenre_GameProducts_GameProductsId",
                        column: x => x.GameProductsId,
                        principalTable: "GameProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameProductGenre_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameProductGenre_GenreId",
                table: "GameProductGenre",
                column: "GenreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameProductGenre");

            migrationBuilder.AddColumn<int>(
                name: "GenreId",
                table: "GameProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GameProducts_GenreId",
                table: "GameProducts",
                column: "GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameProducts_Genres_GenreId",
                table: "GameProducts",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
