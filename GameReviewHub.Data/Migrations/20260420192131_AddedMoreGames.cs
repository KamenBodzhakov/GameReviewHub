using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GameReviewHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedMoreGames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Description", "Developer", "ImagePath", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { 7, "An expansive open-world RPG following Geralt of Rivia, featuring deep storytelling, memorable characters, and meaningful choices.", "CD Projekt Red", "/images/games/witcher-3.jpg", new DateTime(2015, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Witcher 3: Wild Hunt" },
                    { 8, "A challenging open-world action RPG blending Souls-like combat with exploration in a vast fantasy world.", "FromSoftware", "/images/games/elden-ring.jpg", new DateTime(2022, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Elden Ring" },
                    { 9, "A dark fantasy city builder with roguelike elements where players rebuild civilization under constant threat of destruction.", "Eremite Games", "/images/games/against-the-storm.jpg", new DateTime(2023, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Against the Storm" },
                    { 10, "A turn-based roguelike road trip through a decaying world, combining strategic combat with psychological stress mechanics.", "Red Hook Studios", "/images/games/darkest-dungeon-2.jpg", new DateTime(2023, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Darkest Dungeon II" },
                    { 11, "An action roguelite with fast-paced combat and dreamlike visuals, focused on skill-based gameplay and replayability.", "NEOWIZ", "/images/games/shape-of-dreams.jpg", new DateTime(2024, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Shape of Dreams" },
                    { 12, "A deep tactical RPG featuring cooperative gameplay, rich storytelling, and highly interactive environments.", "Larian Studios", "/images/games/divinity-original-sin-2.jpg", new DateTime(2017, 9, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Divinity: Original Sin 2" },
                    { 13, "An open-world action RPG set in the Wizarding World, allowing players to explore Hogwarts and master magic.", "Avalanche Software", "/images/games/hogwarts-legacy.jpg", new DateTime(2023, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hogwarts Legacy" },
                    { 14, "A fast-paced looter shooter with cooperative gameplay, outrageous weapons, and a stylized sci-fi world.", "Gearbox Software", "/images/games/borderlands-3.jpg", new DateTime(2019, 9, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Borderlands 3" },
                    { 15, "A multiplayer open-world survival game set in a post-apocalyptic world with supernatural elements and base-building mechanics.", "Starry Studio", "/images/games/once-human.jpg", new DateTime(2024, 7, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Once Human" }
                });

            migrationBuilder.InsertData(
                table: "GamesGenres",
                columns: new[] { "GameId", "GenreId" },
                values: new object[,]
                {
                    { 6, 4 },
                    { 6, 5 },
                    { 6, 7 },
                    { 7, 1 },
                    { 7, 4 },
                    { 7, 7 },
                    { 8, 1 },
                    { 8, 5 },
                    { 8, 7 },
                    { 9, 6 },
                    { 9, 9 },
                    { 9, 12 },
                    { 10, 2 },
                    { 10, 6 },
                    { 10, 9 },
                    { 11, 5 },
                    { 11, 6 },
                    { 12, 1 },
                    { 12, 2 },
                    { 12, 3 },
                    { 12, 4 },
                    { 13, 1 },
                    { 13, 4 },
                    { 13, 7 },
                    { 14, 4 },
                    { 14, 5 },
                    { 14, 11 },
                    { 15, 5 },
                    { 15, 7 },
                    { 15, 17 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GamesGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { 6, 4 });

            migrationBuilder.DeleteData(
                table: "GamesGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { 6, 5 });

            migrationBuilder.DeleteData(
                table: "GamesGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { 6, 7 });

            migrationBuilder.DeleteData(
                table: "GamesGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { 7, 1 });

            migrationBuilder.DeleteData(
                table: "GamesGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { 7, 4 });

            migrationBuilder.DeleteData(
                table: "GamesGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { 7, 7 });

            migrationBuilder.DeleteData(
                table: "GamesGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { 8, 1 });

            migrationBuilder.DeleteData(
                table: "GamesGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { 8, 5 });

            migrationBuilder.DeleteData(
                table: "GamesGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { 8, 7 });

            migrationBuilder.DeleteData(
                table: "GamesGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { 9, 6 });

            migrationBuilder.DeleteData(
                table: "GamesGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { 9, 9 });

            migrationBuilder.DeleteData(
                table: "GamesGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { 9, 12 });

            migrationBuilder.DeleteData(
                table: "GamesGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { 10, 2 });

            migrationBuilder.DeleteData(
                table: "GamesGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { 10, 6 });

            migrationBuilder.DeleteData(
                table: "GamesGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { 10, 9 });

            migrationBuilder.DeleteData(
                table: "GamesGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { 11, 5 });

            migrationBuilder.DeleteData(
                table: "GamesGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { 11, 6 });

            migrationBuilder.DeleteData(
                table: "GamesGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { 12, 1 });

            migrationBuilder.DeleteData(
                table: "GamesGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { 12, 2 });

            migrationBuilder.DeleteData(
                table: "GamesGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { 12, 3 });

            migrationBuilder.DeleteData(
                table: "GamesGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { 12, 4 });

            migrationBuilder.DeleteData(
                table: "GamesGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { 13, 1 });

            migrationBuilder.DeleteData(
                table: "GamesGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { 13, 4 });

            migrationBuilder.DeleteData(
                table: "GamesGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { 13, 7 });

            migrationBuilder.DeleteData(
                table: "GamesGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { 14, 4 });

            migrationBuilder.DeleteData(
                table: "GamesGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { 14, 5 });

            migrationBuilder.DeleteData(
                table: "GamesGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { 14, 11 });

            migrationBuilder.DeleteData(
                table: "GamesGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { 15, 5 });

            migrationBuilder.DeleteData(
                table: "GamesGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { 15, 7 });

            migrationBuilder.DeleteData(
                table: "GamesGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { 15, 17 });

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 15);
        }
    }
}
