using GameReviewHub.Data;
using GameReviewHub.Data.Models;
using GameReviewHub.Services.Core;
using GameReviewHub.ViewModels.ReviewComment;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GameReviewHub.Services.Tests
{
    [TestFixture]
    public class ReviewCommentServiceTests
    {
        private GameReviewHubDbContext dbContext = null!;
        private ReviewCommentService reviewCommentService = null!;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<GameReviewHubDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            dbContext = new GameReviewHubDbContext(options);
            reviewCommentService = new ReviewCommentService(dbContext);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        [Test]
        public async Task AddCommentAsync_ShouldReturnFalse_WhenReviewDoesNotExist()
        {
            CreateReviewCommentInputModel input = new CreateReviewCommentInputModel
            {
                Body = "This is a valid comment."
            };

            bool result = await reviewCommentService.AddCommentAsync(999, input, "user-1");

            int commentsCount = await dbContext.ReviewComments.CountAsync();

            Assert.That(result, Is.False);
            Assert.That(commentsCount, Is.EqualTo(0));
        }

        [Test]
        public async Task AddCommentAsync_ShouldAddCommentSuccessfully_WhenReviewExists()
        {
            Review review = new Review
            {
                Id = 1,
                Title = "Test Review",
                Body = "This is a valid review body.",
                Rating = 8,
                GameId = 1,
                UserId = "user-1",
                CreatedOn = DateTime.UtcNow
            };

            dbContext.Reviews.Add(review);
            await dbContext.SaveChangesAsync();

            CreateReviewCommentInputModel input = new CreateReviewCommentInputModel
            {
                Body = "This is a valid comment."
            };

            bool result = await reviewCommentService.AddCommentAsync(1, input, "user-2");

            ReviewComment? comment = await dbContext.ReviewComments
                .FirstOrDefaultAsync(c => c.ReviewId == 1 && c.UserId == "user-2");

            Assert.That(result, Is.True);
            Assert.That(comment, Is.Not.Null);
            Assert.That(comment!.Body, Is.EqualTo("This is a valid comment."));
            Assert.That(comment.ReviewId, Is.EqualTo(1));
            Assert.That(comment.UserId, Is.EqualTo("user-2"));
        }

        [Test]
        public async Task GetCommentsByReviewIdAsync_ShouldReturnEmptyList_WhenNoCommentsExist()
        {
            List<ReviewCommentViewModel> result = await reviewCommentService.GetCommentsByReviewIdAsync(1);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task GetCommentsByReviewIdAsync_ShouldReturnCommentsOrderedByCreatedOn()
        {
            IdentityUser firstUser = new IdentityUser
            {
                Id = "user-1",
                UserName = "firstUser",
                NormalizedUserName = "FIRSTUSER",
                Email = "first@test.com",
                NormalizedEmail = "FIRST@TEST.COM"
            };

            IdentityUser secondUser = new IdentityUser
            {
                Id = "user-2",
                UserName = "secondUser",
                NormalizedUserName = "SECONDUSER",
                Email = "second@test.com",
                NormalizedEmail = "SECOND@TEST.COM"
            };

            ReviewComment firstComment = new ReviewComment
            {
                Id = 1,
                Body = "First comment",
                ReviewId = 1,
                UserId = "user-1",
                User = firstUser,
                CreatedOn = new DateTime(2024, 1, 1)
            };

            ReviewComment secondComment = new ReviewComment
            {
                Id = 2,
                Body = "Second comment",
                ReviewId = 1,
                UserId = "user-2",
                User = secondUser,
                CreatedOn = new DateTime(2024, 1, 2)
            };

            dbContext.Users.AddRange(firstUser, secondUser);
            dbContext.ReviewComments.AddRange(firstComment, secondComment);

            await dbContext.SaveChangesAsync();

            List<ReviewCommentViewModel> result = await reviewCommentService.GetCommentsByReviewIdAsync(1);

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].Body, Is.EqualTo("First comment"));
            Assert.That(result[0].AuthorUserId, Is.EqualTo("user-1"));
            Assert.That(result[0].AuthorUserName, Is.EqualTo("firstUser"));
            Assert.That(result[1].Body, Is.EqualTo("Second comment"));
            Assert.That(result[1].AuthorUserId, Is.EqualTo("user-2"));
            Assert.That(result[1].AuthorUserName, Is.EqualTo("secondUser"));
        }
    }
}
