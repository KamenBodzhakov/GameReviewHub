using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameReviewHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class ShortenedRDR2Description : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 6,
                column: "Description",
                value: "An epic story set at the dawn of modern America. Follow Arthur Morgan and the Van der Linde gang as they struggle to survive in a changing world.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 6,
                column: "Description",
                value: "An epic tale of life in America at the dawn of the modern age. Follow Arthur Morgan and the Van der Linde gang as they struggle to survive in a rapidly changing world.");
        }
    }
}
