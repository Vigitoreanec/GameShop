using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameShopModel.Migrations
{
    /// <inheritdoc />
    public partial class Add_SistemRequirement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MinimumSystemRequirement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Processor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RandomAccessMemory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VideoCard = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DirectX = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiskSpace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoundCard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Network = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Additionally = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GameProductId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MinimumSystemRequirement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MinimumSystemRequirement_GameProducts_GameProductId",
                        column: x => x.GameProductId,
                        principalTable: "GameProducts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RecommendedSystemRequirement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Processor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RandomAccessMemory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VideoCard = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DirectX = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiskSpace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoundCard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Network = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Additionally = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GameProductId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecommendedSystemRequirement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecommendedSystemRequirement_GameProducts_GameProductId",
                        column: x => x.GameProductId,
                        principalTable: "GameProducts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MinimumSystemRequirement_GameProductId",
                table: "MinimumSystemRequirement",
                column: "GameProductId");

            migrationBuilder.CreateIndex(
                name: "IX_RecommendedSystemRequirement_GameProductId",
                table: "RecommendedSystemRequirement",
                column: "GameProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MinimumSystemRequirement");

            migrationBuilder.DropTable(
                name: "RecommendedSystemRequirement");
        }
    }
}
