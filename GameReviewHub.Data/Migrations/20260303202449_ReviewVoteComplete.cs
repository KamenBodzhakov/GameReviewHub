using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameReviewHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class ReviewVoteComplete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewVote_AspNetUsers_UserId",
                table: "ReviewVote");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewVote_Reviews_ReviewId",
                table: "ReviewVote");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReviewVote",
                table: "ReviewVote");

            migrationBuilder.RenameTable(
                name: "ReviewVote",
                newName: "ReviewVotes");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewVote_UserId",
                table: "ReviewVotes",
                newName: "IX_ReviewVotes_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewVote_ReviewId_UserId",
                table: "ReviewVotes",
                newName: "IX_ReviewVotes_ReviewId_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReviewVotes",
                table: "ReviewVotes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewVotes_AspNetUsers_UserId",
                table: "ReviewVotes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewVotes_Reviews_ReviewId",
                table: "ReviewVotes",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewVotes_AspNetUsers_UserId",
                table: "ReviewVotes");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewVotes_Reviews_ReviewId",
                table: "ReviewVotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReviewVotes",
                table: "ReviewVotes");

            migrationBuilder.RenameTable(
                name: "ReviewVotes",
                newName: "ReviewVote");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewVotes_UserId",
                table: "ReviewVote",
                newName: "IX_ReviewVote_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewVotes_ReviewId_UserId",
                table: "ReviewVote",
                newName: "IX_ReviewVote_ReviewId_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReviewVote",
                table: "ReviewVote",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewVote_AspNetUsers_UserId",
                table: "ReviewVote",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewVote_Reviews_ReviewId",
                table: "ReviewVote",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "Id");
        }
    }
}
