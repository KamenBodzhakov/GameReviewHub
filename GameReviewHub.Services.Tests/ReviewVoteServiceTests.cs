using GameReviewHub.Data;
using GameReviewHub.Data.Models;
using GameReviewHub.Services.Core;
using Microsoft.EntityFrameworkCore;


namespace GameReviewHub.Services.Tests
{
    [TestFixture]
    public class ReviewVoteServiceTests
    {
        private GameReviewHubDbContext dbContext = null!;
        private ReviewVoteService reviewVoteService = null!;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<GameReviewHubDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            dbContext = new GameReviewHubDbContext(options);
            reviewVoteService = new ReviewVoteService(dbContext);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        [Test]
        public async Task GetVoteCountAsync_ShouldReturnZero_WhenNoUpvotesExist()
        {
            int result = await reviewVoteService.GetVoteCountAsync(1);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public async Task GetVoteCountAsync_ShouldReturnOnlyUpvotesCount()
        {
            dbContext.ReviewVotes.AddRange(new List<ReviewVote>
            {
                new ReviewVote { ReviewId = 1, UserId = "user-1", IsUpvote = true },
                new ReviewVote { ReviewId = 1, UserId = "user-2", IsUpvote = true },
                new ReviewVote { ReviewId = 1, UserId = "user-3", IsUpvote = false },
                new ReviewVote { ReviewId = 2, UserId = "user-4", IsUpvote = true }
            });

            await dbContext.SaveChangesAsync();

            int result = await reviewVoteService.GetVoteCountAsync(1);

            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public async Task HasUserVotedAsync_ShouldReturnFalse_WhenUserHasNotVoted()
        {
            bool result = await reviewVoteService.HasUserVotedAsync(1, "user-1");

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task HasUserVotedAsync_ShouldReturnTrue_WhenUserHasVoted()
        {
            dbContext.ReviewVotes.Add(new ReviewVote
            {
                ReviewId = 1,
                UserId = "user-1",
                IsUpvote = true
            });

            await dbContext.SaveChangesAsync();

            bool result = await reviewVoteService.HasUserVotedAsync(1, "user-1");

            Assert.That(result, Is.True);
        }

        [Test]
        public async Task VoteAsync_ShouldReturnFalse_WhenReviewDoesNotExist()
        {
            bool result = await reviewVoteService.VoteAsync(999, "user-1", true);

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task VoteAsync_ShouldAddVoteSuccessfully_WhenUserHasNotVotedYet()
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

            bool result = await reviewVoteService.VoteAsync(1, "user-2", true);

            ReviewVote? vote = await dbContext.ReviewVotes
                .FirstOrDefaultAsync(rv => rv.ReviewId == 1 && rv.UserId == "user-2");

            Assert.That(result, Is.True);
            Assert.That(vote, Is.Not.Null);
            Assert.That(vote!.IsUpvote, Is.True);
        }

        [Test]
        public async Task VoteAsync_ShouldReturnFalse_WhenSameVoteIsSubmittedAgain()
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

            ReviewVote vote = new ReviewVote
            {
                ReviewId = 1,
                UserId = "user-2",
                IsUpvote = true
            };

            dbContext.Reviews.Add(review);
            dbContext.ReviewVotes.Add(vote);
            await dbContext.SaveChangesAsync();

            bool result = await reviewVoteService.VoteAsync(1, "user-2", true);

            int votesCount = await dbContext.ReviewVotes.CountAsync();

            Assert.That(result, Is.False);
            Assert.That(votesCount, Is.EqualTo(1));
        }

        [Test]
        public async Task VoteAsync_ShouldUpdateVote_WhenUserChangesVote()
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

            ReviewVote vote = new ReviewVote
            {
                ReviewId = 1,
                UserId = "user-2",
                IsUpvote = true
            };

            dbContext.Reviews.Add(review);
            dbContext.ReviewVotes.Add(vote);
            await dbContext.SaveChangesAsync();

            bool result = await reviewVoteService.VoteAsync(1, "user-2", false);

            ReviewVote? updatedVote = await dbContext.ReviewVotes
                .FirstOrDefaultAsync(rv => rv.ReviewId == 1 && rv.UserId == "user-2");

            Assert.That(result, Is.True);
            Assert.That(updatedVote, Is.Not.Null);
            Assert.That(updatedVote!.IsUpvote, Is.False);
        }
    }
}
