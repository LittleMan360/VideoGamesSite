using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VideoGamesSite.Migrations
{
    /// <inheritdoc />
    public partial class AddTopGames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TopGames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Tag = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Rank = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopGames", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TopGames",
                columns: new[] { "Id", "Rank", "Tag", "Title" },
                values: new object[,]
                {
                    { 1, 1, "FPS", "Halo: Reach" },
                    { 2, 2, "FPS", "Halo 3" },
                    { 3, 3, "TPS", "Gears of War 2" },
                    { 4, 4, "Looter Shooter", "Borderlands 2" },
                    { 5, 5, "FPS", "Call of Duty: Black Ops II" },
                    { 6, 6, "RPG", "The Witcher 3: Wild Hunt" },
                    { 7, 7, "CRPG", "Baldur's Gate 3" },
                    { 8, 8, "Action RPG", "Elden Ring" },
                    { 9, 9, "Colony Sim", "RimWorld" },
                    { 10, 10, "Platformer", "Psychonauts" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TopGames");
        }
    }
}
