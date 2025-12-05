using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VideoGamesSite.Migrations
{
    /// <inheritdoc />
    public partial class AddNowPlayingItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NowPlayingItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Platform = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Focus = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Progress = table.Column<int>(type: "int", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NowPlayingItems", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "NowPlayingItems",
                columns: new[] { "Id", "DisplayOrder", "Focus", "Platform", "Progress", "Title" },
                values: new object[,]
                {
                    { 1, 1, "Beating story with girlfiend", "Xbox", 20, "Halo CE" },
                    { 2, 2, "Beating the bosses", "PC", 0, "Ark Survival Ascended" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NowPlayingItems");
        }
    }
}
