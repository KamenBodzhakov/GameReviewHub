using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameReviewHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class changingToCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewVotes_Reviews_ReviewId",
                table: "ReviewVotes");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewVotes_Reviews_ReviewId",
                table: "ReviewVotes",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewVotes_Reviews_ReviewId",
                table: "ReviewVotes");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewVotes_Reviews_ReviewId",
                table: "ReviewVotes",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "Id");
        }
    }
}
