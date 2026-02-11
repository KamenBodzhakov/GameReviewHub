using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GameReviewHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Description", "Developer", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { 1, "A sprawling role-playing adventure in the Dungeons & Dragons universe, featuring deep narrative, tactical combat, and rich character progression.", "Larian Studios", new DateTime(2023, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Baldur's Gate 3" },
                    { 2, "A fast-paced roguelike action game where players fight to escape the Underworld, combining tight combat with evolving storytelling.", "Supergiant Games", new DateTime(2020, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hades" },
                    { 3, "A wuxia-inspired open-world action-adventure RPG set in a fantastical version of historical China, blending martial arts combat with exploration.", "Everstone Studio", new DateTime(2025, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Where Winds Meet" },
                    { 4, "A visually stunning platform adventure about a young guardian spirit on a touching journey to save a dying forest filled with heart and challenge.", "Moon Studios", new DateTime(2015, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ori and the Blind Forest" },
                    { 5, "A beloved roguelike deckbuilder that blends strategic card combat with procedural levels, challenging players to ascend the mysterious Spire.", "Mega Crit", new DateTime(2019, 1, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Slay the Spire" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "RPG" },
                    { 2, "Turn-Based" },
                    { 3, "Tactical" },
                    { 4, "Adventure" },
                    { 5, "Action" },
                    { 6, "Roguelike" },
                    { 7, "Open World" },
                    { 8, "Platformer" },
                    { 9, "Strategy" },
                    { 10, "Deckbuilder" }
                });

            migrationBuilder.InsertData(
                table: "GamesGenres",
                columns: new[] { "GameId", "GenreId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 1, 4 },
                    { 2, 5 },
                    { 2, 6 },
                    { 3, 1 },
                    { 3, 4 },
                    { 3, 5 },
                    { 3, 7 },
                    { 4, 4 },
                    { 4, 8 },
                    { 5, 6 },
                    { 5, 9 },
                    { 5, 10 }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Body", "CreatedOn", "GameId", "Score", "Title" },
                values: new object[,]
                {
                    { 1, "Pros:\n- Deep role-playing systems and meaningful choices\n- Strong companion writing and voice acting\n- Great tactical combat variety\n\nCons:\n- Can feel overwhelming early on\n- Occasional pacing dips in long sessions\n\nOverall:\n- An exceptional RPG that rewards exploration and experimentation.", new DateTime(2023, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 10, "A modern RPG benchmark" },
                    { 2, "Pros:\n- Tight combat and excellent weapon variety\n- Fantastic music, art direction, and character dialogue\n- Progression feels rewarding run after run\n\nCons:\n- Repetition can set in if you binge it\n- Some builds depend heavily on luck\n\nOverall:\n- One of the best roguelikes, with story that actually motivates retries.", new DateTime(2020, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 9, "Fast, stylish, endlessly replayable" },
                    { 3, "Pros:\n- Beautiful world design and strong wuxia atmosphere\n- Martial arts combat feels unique and expressive\n- Exploration and movement are very satisfying\n\nCons:\n- Performance can be inconsistent\n- Some systems feel under-explained\n\nOverall:\n- An ambitious action RPG that could be great with refinement and updates.", new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 8, "Huge potential, needs polish" },
                    { 4, "Pros:\n- Stunning visuals and memorable soundtrack\n- Fluid movement and satisfying platforming\n- Strong emotional storytelling\n\nCons:\n- A few sections have sharp difficulty spikes\n- Combat is simpler than the platforming\n\nOverall:\n- A gorgeous adventure that’s worth experiencing for its world and feel.", new DateTime(2015, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 9, "Beautiful and emotional platforming" },
                    { 5, "Pros:\n- Deep strategy with tons of build variety\n- Highly replayable with meaningful decisions\n- Excellent balance and difficulty progression\n\nCons:\n- Visual presentation is minimalistic\n- Runs can swing based on RNG\n\nOverall:\n- A top-tier deckbuilder that stays fresh for hundreds of runs.", new DateTime(2019, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 9, "Strategic deckbuilding perfection" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GamesGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "GamesGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "GamesGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "GamesGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "GamesGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { 2, 5 });

            migrationBuilder.DeleteData(
                table: "GamesGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { 2, 6 });

            migrationBuilder.DeleteData(
                table: "GamesGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "GamesGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { 3, 4 });

            migrationBuilder.DeleteData(
                table: "GamesGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { 3, 5 });

            migrationBuilder.DeleteData(
                table: "GamesGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { 3, 7 });

            migrationBuilder.DeleteData(
                table: "GamesGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { 4, 4 });

            migrationBuilder.DeleteData(
                table: "GamesGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { 4, 8 });

            migrationBuilder.DeleteData(
                table: "GamesGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { 5, 6 });

            migrationBuilder.DeleteData(
                table: "GamesGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { 5, 9 });

            migrationBuilder.DeleteData(
                table: "GamesGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { 5, 10 });

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
