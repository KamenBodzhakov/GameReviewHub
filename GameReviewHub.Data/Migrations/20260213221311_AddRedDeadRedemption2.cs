using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameReviewHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRedDeadRedemption2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Description", "Developer", "ImagePath", "ReleaseDate", "Title" },
                values: new object[] { 6, "An epic tale of life in America at the dawn of the modern age. Follow Arthur Morgan and the Van der Linde gang as they struggle to survive in a rapidly changing world.", "Rockstar Games", "/images/games/red-dead-redemption-2.jpg", new DateTime(2018, 10, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Red Dead Redemption 2" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
