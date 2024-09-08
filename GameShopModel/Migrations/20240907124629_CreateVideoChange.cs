using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameShopModel.Migrations
{
    /// <inheritdoc />
    public partial class CreateVideoChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GameProductId",
                table: "Videos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Videos_GameProductId",
                table: "Videos",
                column: "GameProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_GameProducts_GameProductId",
                table: "Videos",
                column: "GameProductId",
                principalTable: "GameProducts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videos_GameProducts_GameProductId",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Videos_GameProductId",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "GameProductId",
                table: "Videos");
        }
    }
}
