using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameShopModel.Migrations
{
    /// <inheritdoc />
    public partial class RecommendedGameProductsList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RecommendedGameProductsId",
                table: "GameProducts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RecommendedGameProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpertName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpertSurname = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecommendedGameProducts", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameProducts_RecommendedGameProducts_RecommendedGameProductsId",
                table: "GameProducts");

            migrationBuilder.DropTable(
                name: "RecommendedGameProducts");

            migrationBuilder.DropIndex(
                name: "IX_GameProducts_RecommendedGameProductsId",
                table: "GameProducts");

            migrationBuilder.DropColumn(
                name: "RecommendedGameProductsId",
                table: "GameProducts");
        }
    }
}
