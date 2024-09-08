using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameShopModel.Migrations
{
    /// <inheritdoc />
    public partial class CreateVideoChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameProductGenre_Genres_GenreId",
                table: "GameProductGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_MinimumSystemRequirements_GameProducts_GameProductId",
                table: "MinimumSystemRequirements");

            migrationBuilder.DropForeignKey(
                name: "FK_RecommendedSystemRequirements_GameProducts_GameProductId",
                table: "RecommendedSystemRequirements");

            migrationBuilder.DropIndex(
                name: "IX_RecommendedSystemRequirements_GameProductId",
                table: "RecommendedSystemRequirements");

            migrationBuilder.DropIndex(
                name: "IX_MinimumSystemRequirements_GameProductId",
                table: "MinimumSystemRequirements");

            migrationBuilder.DropColumn(
                name: "GameProductId",
                table: "RecommendedSystemRequirements");

            migrationBuilder.DropColumn(
                name: "GameProductId",
                table: "MinimumSystemRequirements");

            migrationBuilder.RenameColumn(
                name: "GenreId",
                table: "GameProductGenre",
                newName: "GenresId");

            migrationBuilder.RenameIndex(
                name: "IX_GameProductGenre_GenreId",
                table: "GameProductGenre",
                newName: "IX_GameProductGenre_GenresId");

            migrationBuilder.AddColumn<int>(
                name: "MinimumSystemRequirementsId",
                table: "GameProducts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecommendedSystemRequirementsId",
                table: "GameProducts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameProducts_MinimumSystemRequirementsId",
                table: "GameProducts",
                column: "MinimumSystemRequirementsId");

            migrationBuilder.CreateIndex(
                name: "IX_GameProducts_RecommendedSystemRequirementsId",
                table: "GameProducts",
                column: "RecommendedSystemRequirementsId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameProductGenre_Genres_GenresId",
                table: "GameProductGenre",
                column: "GenresId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameProducts_MinimumSystemRequirements_MinimumSystemRequirementsId",
                table: "GameProducts",
                column: "MinimumSystemRequirementsId",
                principalTable: "MinimumSystemRequirements",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GameProducts_RecommendedSystemRequirements_RecommendedSystemRequirementsId",
                table: "GameProducts",
                column: "RecommendedSystemRequirementsId",
                principalTable: "RecommendedSystemRequirements",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameProductGenre_Genres_GenresId",
                table: "GameProductGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_GameProducts_MinimumSystemRequirements_MinimumSystemRequirementsId",
                table: "GameProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_GameProducts_RecommendedSystemRequirements_RecommendedSystemRequirementsId",
                table: "GameProducts");

            migrationBuilder.DropIndex(
                name: "IX_GameProducts_MinimumSystemRequirementsId",
                table: "GameProducts");

            migrationBuilder.DropIndex(
                name: "IX_GameProducts_RecommendedSystemRequirementsId",
                table: "GameProducts");

            migrationBuilder.DropColumn(
                name: "MinimumSystemRequirementsId",
                table: "GameProducts");

            migrationBuilder.DropColumn(
                name: "RecommendedSystemRequirementsId",
                table: "GameProducts");

            migrationBuilder.RenameColumn(
                name: "GenresId",
                table: "GameProductGenre",
                newName: "GenreId");

            migrationBuilder.RenameIndex(
                name: "IX_GameProductGenre_GenresId",
                table: "GameProductGenre",
                newName: "IX_GameProductGenre_GenreId");

            migrationBuilder.AddColumn<int>(
                name: "GameProductId",
                table: "RecommendedSystemRequirements",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GameProductId",
                table: "MinimumSystemRequirements",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecommendedSystemRequirements_GameProductId",
                table: "RecommendedSystemRequirements",
                column: "GameProductId");

            migrationBuilder.CreateIndex(
                name: "IX_MinimumSystemRequirements_GameProductId",
                table: "MinimumSystemRequirements",
                column: "GameProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameProductGenre_Genres_GenreId",
                table: "GameProductGenre",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MinimumSystemRequirements_GameProducts_GameProductId",
                table: "MinimumSystemRequirements",
                column: "GameProductId",
                principalTable: "GameProducts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecommendedSystemRequirements_GameProducts_GameProductId",
                table: "RecommendedSystemRequirements",
                column: "GameProductId",
                principalTable: "GameProducts",
                principalColumn: "Id");
        }
    }
}
