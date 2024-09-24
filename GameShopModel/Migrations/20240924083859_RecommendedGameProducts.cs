using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameShopModel.Migrations
{
    /// <inheritdoc />
    public partial class RecommendedGameProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameProducts_RecommendedGameProducts_RecommendedGameProductsId",
                table: "GameProducts");

            migrationBuilder.DropIndex(
                name: "IX_GameProducts_RecommendedGameProductsId",
                table: "GameProducts");

            migrationBuilder.DropColumn(
                name: "RecommendedGameProductsId",
                table: "GameProducts");

            migrationBuilder.AddColumn<int>(
                name: "GameProductId",
                table: "RecommendedGameProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RecommendedGameProducts_GameProductId",
                table: "RecommendedGameProducts",
                column: "GameProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecommendedGameProducts_GameProducts_GameProductId",
                table: "RecommendedGameProducts",
                column: "GameProductId",
                principalTable: "GameProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecommendedGameProducts_GameProducts_GameProductId",
                table: "RecommendedGameProducts");

            migrationBuilder.DropIndex(
                name: "IX_RecommendedGameProducts_GameProductId",
                table: "RecommendedGameProducts");

            migrationBuilder.DropColumn(
                name: "GameProductId",
                table: "RecommendedGameProducts");

            migrationBuilder.AddColumn<int>(
                name: "RecommendedGameProductsId",
                table: "GameProducts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameProducts_RecommendedGameProductsId",
                table: "GameProducts",
                column: "RecommendedGameProductsId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameProducts_RecommendedGameProducts_RecommendedGameProductsId",
                table: "GameProducts",
                column: "RecommendedGameProductsId",
                principalTable: "RecommendedGameProducts",
                principalColumn: "Id");
        }
    }
}
