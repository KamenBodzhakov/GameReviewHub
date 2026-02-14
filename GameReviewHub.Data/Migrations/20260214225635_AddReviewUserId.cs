using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GameReviewHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddReviewUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Reviews",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_UserId",
                table: "Reviews",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_UserId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Reviews");

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Body", "CreatedOn", "GameId", "Rating", "Title" },
                values: new object[,]
                {
                    { 1, "Pros:\n- Deep role-playing systems and meaningful choices\n- Strong companion writing and voice acting\n- Great tactical combat variety\n\nCons:\n- Can feel overwhelming early on\n- Occasional pacing dips in long sessions\n\nOverall:\n- An exceptional RPG that rewards exploration and experimentation.", new DateTime(2023, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 10, "A modern RPG benchmark" },
                    { 2, "Pros:\n- Tight combat and excellent weapon variety\n- Fantastic music, art direction, and character dialogue\n- Progression feels rewarding run after run\n\nCons:\n- Repetition can set in if you binge it\n- Some builds depend heavily on luck\n\nOverall:\n- One of the best roguelikes, with story that actually motivates retries.", new DateTime(2020, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 9, "Fast, stylish, endlessly replayable" },
                    { 3, "Pros:\n- Beautiful world design and strong wuxia atmosphere\n- Martial arts combat feels unique and expressive\n- Exploration and movement are very satisfying\n\nCons:\n- Performance can be inconsistent\n- Some systems feel under-explained\n\nOverall:\n- An ambitious action RPG that could be great with refinement and updates.", new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 8, "Huge potential, needs polish" },
                    { 4, "Pros:\n- Stunning visuals and memorable soundtrack\n- Fluid movement and satisfying platforming\n- Strong emotional storytelling\n\nCons:\n- A few sections have sharp difficulty spikes\n- Combat is simpler than the platforming\n\nOverall:\n- A gorgeous adventure that’s worth experiencing for its world and feel.", new DateTime(2015, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 9, "Beautiful and emotional platforming" },
                    { 5, "Pros:\n- Deep strategy with tons of build variety\n- Highly replayable with meaningful decisions\n- Excellent balance and difficulty progression\n\nCons:\n- Visual presentation is minimalistic\n- Runs can swing based on RNG\n\nOverall:\n- A top-tier deckbuilder that stays fresh for hundreds of runs.", new DateTime(2019, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 9, "Strategic deckbuilding perfection" }
                });
        }
    }
}
