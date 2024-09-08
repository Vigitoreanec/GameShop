using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameShopModel.Migrations
{
    /// <inheritdoc />
    public partial class AddVideo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MinimumSystemRequirement_GameProducts_GameProductId",
                table: "MinimumSystemRequirement");

            migrationBuilder.DropForeignKey(
                name: "FK_RecommendedSystemRequirement_GameProducts_GameProductId",
                table: "RecommendedSystemRequirement");

            migrationBuilder.DropTable(
                name: "ImageUrls");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecommendedSystemRequirement",
                table: "RecommendedSystemRequirement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MinimumSystemRequirement",
                table: "MinimumSystemRequirement");

            migrationBuilder.RenameTable(
                name: "RecommendedSystemRequirement",
                newName: "RecommendedSystemRequirements");

            migrationBuilder.RenameTable(
                name: "MinimumSystemRequirement",
                newName: "MinimumSystemRequirements");

            migrationBuilder.RenameIndex(
                name: "IX_RecommendedSystemRequirement_GameProductId",
                table: "RecommendedSystemRequirements",
                newName: "IX_RecommendedSystemRequirements_GameProductId");

            migrationBuilder.RenameIndex(
                name: "IX_MinimumSystemRequirement_GameProductId",
                table: "MinimumSystemRequirements",
                newName: "IX_MinimumSystemRequirements_GameProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecommendedSystemRequirements",
                table: "RecommendedSystemRequirements",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MinimumSystemRequirements",
                table: "MinimumSystemRequirements",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GameProductId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_GameProducts_GameProductId",
                        column: x => x.GameProductId,
                        principalTable: "GameProducts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Videos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_GameProductId",
                table: "Images",
                column: "GameProductId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MinimumSystemRequirements_GameProducts_GameProductId",
                table: "MinimumSystemRequirements");

            migrationBuilder.DropForeignKey(
                name: "FK_RecommendedSystemRequirements_GameProducts_GameProductId",
                table: "RecommendedSystemRequirements");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Videos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecommendedSystemRequirements",
                table: "RecommendedSystemRequirements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MinimumSystemRequirements",
                table: "MinimumSystemRequirements");

            migrationBuilder.RenameTable(
                name: "RecommendedSystemRequirements",
                newName: "RecommendedSystemRequirement");

            migrationBuilder.RenameTable(
                name: "MinimumSystemRequirements",
                newName: "MinimumSystemRequirement");

            migrationBuilder.RenameIndex(
                name: "IX_RecommendedSystemRequirements_GameProductId",
                table: "RecommendedSystemRequirement",
                newName: "IX_RecommendedSystemRequirement_GameProductId");

            migrationBuilder.RenameIndex(
                name: "IX_MinimumSystemRequirements_GameProductId",
                table: "MinimumSystemRequirement",
                newName: "IX_MinimumSystemRequirement_GameProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecommendedSystemRequirement",
                table: "RecommendedSystemRequirement",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MinimumSystemRequirement",
                table: "MinimumSystemRequirement",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ImageUrls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameProductId = table.Column<int>(type: "int", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageUrls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImageUrls_GameProducts_GameProductId",
                        column: x => x.GameProductId,
                        principalTable: "GameProducts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImageUrls_GameProductId",
                table: "ImageUrls",
                column: "GameProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_MinimumSystemRequirement_GameProducts_GameProductId",
                table: "MinimumSystemRequirement",
                column: "GameProductId",
                principalTable: "GameProducts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecommendedSystemRequirement_GameProducts_GameProductId",
                table: "RecommendedSystemRequirement",
                column: "GameProductId",
                principalTable: "GameProducts",
                principalColumn: "Id");
        }
    }
}
