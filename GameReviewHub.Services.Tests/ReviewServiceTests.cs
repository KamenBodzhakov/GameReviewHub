using GameReviewHub.Data;
using GameReviewHub.Data.Models;
using GameReviewHub.Services.Core;
using GameReviewHub.ViewModels.Review;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GameReviewHub.Services.Tests
{
    [TestFixture]
    public class ReviewServiceTests
    {
        private GameReviewHubDbContext dbContext;
        private ReviewService reviewService;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<GameReviewHubDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            dbContext = new GameReviewHubDbContext(options);

            reviewService = new ReviewService(dbContext);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        [Test]
        public async Task ReviewExistsAsync_ShouldReturnFalse_WhenReviewDoesNotExist()
        {
            bool result = await reviewService.ReviewExistsAsync(999);

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task ReviewExistsAsync_ShouldReturnTrue_WhenReviewExists()
        {
            Game game = new Game
            {
                Id = 1,
                Title = "Hades",
                Developer = "Supergiant Games",
                Description = "A valid long enough description.",
                ReleaseDate = new DateTime(2020, 9, 17)
            };

            IdentityUser user = new IdentityUser
            {
                Id = "user-1",
                UserName = "tester",
                NormalizedUserName = "TESTER",
                Email = "tester@test.com",
                NormalizedEmail = "TESTER@TEST.COM"
            };

            Review review = new Review
            {
                Id = 1,
                Title = "Great Review",
                Body = "A valid review body.",
                Rating = 9,
                GameId = 1,
                Game = game,
                UserId = "user-1",
                User = user,
                CreatedOn = DateTime.UtcNow
            };

            dbContext.Games.Add(game);
            dbContext.Users.Add(user);
            dbContext.Reviews.Add(review);

            await dbContext.SaveChangesAsync();

            bool result = await reviewService.ReviewExistsAsync(1);

            Assert.That(result, Is.True);
        }

        [Test]
        public async Task CreateReviewAsync_ShouldReturnFalse_WhenGameDoesNotExist()
        {
            CreateReviewInputModel input = new CreateReviewInputModel
            {
                Title = "Test Review",
                Body = "This is a valid review body.",
                Rating = 8
            };

            bool result = await reviewService.CreateReviewAsync(999, input, "user-1");

            int reviewsCount = await dbContext.Reviews.CountAsync();

            Assert.That(result, Is.False);
            Assert.That(reviewsCount, Is.EqualTo(0));
        }

        [Test]
        public async Task CreateReviewAsync_ShouldCreateReviewSuccessfully_WhenInputIsValid()
        {
            Game game = new Game
            {
                Id = 1,
                Title = "Hades",
                Developer = "Supergiant Games",
                Description = "A valid long enough description.",
                ReleaseDate = new DateTime(2020, 9, 17)
            };

            IdentityUser user = new IdentityUser
            {
                Id = "user-1",
                UserName = "tester",
                NormalizedUserName = "TESTER",
                Email = "tester@test.com",
                NormalizedEmail = "TESTER@TEST.COM"
            };

            dbContext.Games.Add(game);
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();

            CreateReviewInputModel input = new CreateReviewInputModel
            {
                Title = "Great Review",
                Body = "This is a valid review body.",
                Rating = 9
            };

            bool result = await reviewService.CreateReviewAsync(1, input, "user-1");

            Review? createdReview = await dbContext.Reviews.FirstOrDefaultAsync(r => r.Title == "Great Review");

            Assert.That(result, Is.True);
            Assert.That(createdReview, Is.Not.Null);
            Assert.That(createdReview!.GameId, Is.EqualTo(1));
            Assert.That(createdReview.UserId, Is.EqualTo("user-1"));
            Assert.That(createdReview.Body, Is.EqualTo("This is a valid review body."));
            Assert.That(createdReview.Rating, Is.EqualTo(9));
        }

        [Test]
        public async Task DeleteReviewAsync_ShouldReturnFalse_WhenReviewDoesNotExist()
        {
            bool result = await reviewService.DeleteReviewAsync(1, 999);

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task DeleteReviewAsync_ShouldDeleteReviewSuccessfully_WhenReviewExists()
        {
            Game game = new Game
            {
                Id = 1,
                Title = "Hades",
                Developer = "Supergiant Games",
                Description = "A valid long enough description.",
                ReleaseDate = new DateTime(2020, 9, 17)
            };

            IdentityUser user = new IdentityUser
            {
                Id = "user-1",
                UserName = "tester",
                NormalizedUserName = "TESTER",
                Email = "tester@test.com",
                NormalizedEmail = "TESTER@TEST.COM"
            };

            Review review = new Review
            {
                Id = 1,
                Title = "Great Review",
                Body = "This is a valid review body.",
                Rating = 9,
                GameId = 1,
                Game = game,
                UserId = "user-1",
                User = user,
                CreatedOn = DateTime.UtcNow
            };

            dbContext.Games.Add(game);
            dbContext.Users.Add(user);
            dbContext.Reviews.Add(review);

            await dbContext.SaveChangesAsync();

            bool result = await reviewService.DeleteReviewAsync(1, 1);

            int reviewsCount = await dbContext.Reviews.CountAsync();

            Assert.That(result, Is.True);
            Assert.That(reviewsCount, Is.EqualTo(0));
        }

        [Test]
        public async Task ConfirmEditReviewAsync_ShouldReturnFalse_WhenReviewDoesNotExist()
        {
            CreateReviewInputModel input = new CreateReviewInputModel
            {
                Title = "Updated Review",
                Body = "This is an updated valid review body.",
                Rating = 7
            };

            bool result = await reviewService.ConfirmEditReviewAsync(1, 999, input);

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task ConfirmEditReviewAsync_ShouldUpdateReviewSuccessfully_WhenReviewExists()
        {
            Game game = new Game
            {
                Id = 1,
                Title = "Hades",
                Developer = "Supergiant Games",
                Description = "A valid long enough description.",
                ReleaseDate = new DateTime(2020, 9, 17)
            };

            IdentityUser user = new IdentityUser
            {
                Id = "user-1",
                UserName = "tester",
                NormalizedUserName = "TESTER",
                Email = "tester@test.com",
                NormalizedEmail = "TESTER@TEST.COM"
            };

            Review review = new Review
            {
                Id = 1,
                Title = "Old Review",
                Body = "This is an old valid review body.",
                Rating = 5,
                GameId = 1,
                Game = game,
                UserId = "user-1",
                User = user,
                CreatedOn = DateTime.UtcNow
            };

            dbContext.Games.Add(game);
            dbContext.Users.Add(user);
            dbContext.Reviews.Add(review);

            await dbContext.SaveChangesAsync();

            CreateReviewInputModel input = new CreateReviewInputModel
            {
                Title = "Updated Review",
                Body = "This is an updated valid review body.",
                Rating = 9
            };

            bool result = await reviewService.ConfirmEditReviewAsync(1, 1, input);

            Review? updatedReview = await dbContext.Reviews.FirstOrDefaultAsync(r => r.Id == 1);

            Assert.That(result, Is.True);
            Assert.That(updatedReview, Is.Not.Null);
            Assert.That(updatedReview!.Title, Is.EqualTo("Updated Review"));
            Assert.That(updatedReview.Body, Is.EqualTo("This is an updated valid review body."));
            Assert.That(updatedReview.Rating, Is.EqualTo(9));
        }

        [Test]
        public async Task BuildCreateReviewViewModelAsync_ShouldReturnNull_WhenGameDoesNotExist()
        {
            var result = await reviewService.BuildCreateReviewViewModelAsync(999);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task BuildCreateReviewViewModelAsync_ShouldReturnCorrectViewModel_WhenGameExists()
        {
            Game game = new Game
            {
                Id = 1,
                Title = "Hades",
                Developer = "Supergiant Games",
                Description = "A valid long enough description.",
                ReleaseDate = new DateTime(2020, 9, 17)
            };

            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();

            var result = await reviewService.BuildCreateReviewViewModelAsync(1);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.GameId, Is.EqualTo(1));
            Assert.That(result.GameTitle, Is.EqualTo("Hades"));
        }

        [Test]
        public async Task GetReviewsForGameAsync_ShouldReturnNull_WhenGameDoesNotExist()
        {
            GameReviewsViewModel? result = await reviewService.GetReviewsForGameAsync(999);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetReviewsForGameAsync_ShouldReturnCorrectReviewsForGame()
        {
            Game game = new Game
            {
                Id = 1,
                Title = "Hades",
                Developer = "Supergiant Games",
                Description = "A valid long enough description.",
                ReleaseDate = new DateTime(2020, 9, 17)
            };

            IdentityUser user = new IdentityUser
            {
                Id = "user-1",
                UserName = "tester",
                NormalizedUserName = "TESTER",
                Email = "tester@test.com",
                NormalizedEmail = "TESTER@TEST.COM"
            };

            Review review = new Review
            {
                Id = 1,
                Title = "Great Review",
                Body = "This is a valid review body.",
                Rating = 9,
                GameId = 1,
                Game = game,
                UserId = "user-1",
                User = user,
                CreatedOn = DateTime.UtcNow
            };

            dbContext.Games.Add(game);
            dbContext.Users.Add(user);
            dbContext.Reviews.Add(review);

            await dbContext.SaveChangesAsync();

            GameReviewsViewModel? result = await reviewService.GetReviewsForGameAsync(1);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.GameId, Is.EqualTo(1));
            Assert.That(result.GameTitle, Is.EqualTo("Hades"));
            Assert.That(result.Reviews.Count(), Is.EqualTo(1));

            var resultReview = result.Reviews.First();

            Assert.That(resultReview.ReviewId, Is.EqualTo(1));
            Assert.That(resultReview.ReviewTitle, Is.EqualTo("Great Review"));
            Assert.That(resultReview.Body, Is.EqualTo("This is a valid review body."));
            Assert.That(resultReview.Rating, Is.EqualTo(9));
            Assert.That(resultReview.AuthorUserId, Is.EqualTo("user-1"));
            Assert.That(resultReview.AuthorUserName, Is.EqualTo("tester"));
        }

        [Test]
        public async Task BuildDeleteReviewViewModelAsync_ShouldReturnNull_WhenReviewDoesNotExist()
        {
            DeleteReviewViewModel? result = await reviewService.BuildDeleteReviewViewModelAsync(1, 999);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task BuildDeleteReviewViewModelAsync_ShouldReturnCorrectViewModel_WhenReviewExists()
        {
            Game game = new Game
            {
                Id = 1,
                Title = "Hades",
                Developer = "Supergiant Games",
                Description = "A valid long enough description.",
                ReleaseDate = new DateTime(2020, 9, 17)
            };

            IdentityUser user = new IdentityUser
            {
                Id = "user-1",
                UserName = "tester",
                NormalizedUserName = "TESTER",
                Email = "tester@test.com",
                NormalizedEmail = "TESTER@TEST.COM"
            };

            Review review = new Review
            {
                Id = 1,
                Title = "Great Review",
                Body = "This is a valid review body.",
                Rating = 9,
                GameId = 1,
                Game = game,
                UserId = "user-1",
                User = user,
                CreatedOn = new DateTime(2024, 1, 1)
            };

            dbContext.Games.Add(game);
            dbContext.Users.Add(user);
            dbContext.Reviews.Add(review);

            await dbContext.SaveChangesAsync();

            DeleteReviewViewModel? result = await reviewService.BuildDeleteReviewViewModelAsync(1, 1);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.ReviewId, Is.EqualTo(1));
            Assert.That(result.GameId, Is.EqualTo(1));
            Assert.That(result.ReviewTitle, Is.EqualTo("Great Review"));
            Assert.That(result.GameTitle, Is.EqualTo("Hades"));
            Assert.That(result.Rating, Is.EqualTo(9));
            Assert.That(result.CreatedOn, Is.EqualTo(new DateTime(2024, 1, 1)));
        }

        [Test]
        public async Task BuildEditReviewViewModelAsync_ShouldReturnNull_WhenReviewDoesNotExist()
        {
            EditReviewViewModel? result = await reviewService.BuildEditReviewViewModelAsync(1, 999);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task BuildEditReviewViewModelAsync_ShouldReturnCorrectViewModel_WhenReviewExists()
        {
            Game game = new Game
            {
                Id = 1,
                Title = "Hades",
                Developer = "Supergiant Games",
                Description = "A valid long enough description.",
                ReleaseDate = new DateTime(2020, 9, 17)
            };

            IdentityUser user = new IdentityUser
            {
                Id = "user-1",
                UserName = "tester",
                NormalizedUserName = "TESTER",
                Email = "tester@test.com",
                NormalizedEmail = "TESTER@TEST.COM"
            };

            Review review = new Review
            {
                Id = 1,
                Title = "Old Review",
                Body = "This is an old valid review body.",
                Rating = 5,
                GameId = 1,
                Game = game,
                UserId = "user-1",
                User = user,
                CreatedOn = DateTime.UtcNow
            };

            dbContext.Games.Add(game);
            dbContext.Users.Add(user);
            dbContext.Reviews.Add(review);

            await dbContext.SaveChangesAsync();

            EditReviewViewModel? result = await reviewService.BuildEditReviewViewModelAsync(1, 1);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.ReviewId, Is.EqualTo(1));
            Assert.That(result.GameId, Is.EqualTo(1));
            Assert.That(result.GameTitle, Is.EqualTo("Hades"));
            Assert.That(result.Input.Title, Is.EqualTo("Old Review"));
            Assert.That(result.Input.Body, Is.EqualTo("This is an old valid review body."));
            Assert.That(result.Input.Rating, Is.EqualTo(5));
        }

        [Test]
        public async Task GetAllReviewsAsync_ShouldReturnAllReviewsOrderedByCreatedOnDescending()
        {
            Game game = new Game
            {
                Id = 1,
                Title = "Hades",
                Developer = "Supergiant Games",
                Description = "A valid long enough description.",
                ReleaseDate = new DateTime(2020, 9, 17)
            };

            IdentityUser user = new IdentityUser
            {
                Id = "user-1",
                UserName = "tester",
                NormalizedUserName = "TESTER",
                Email = "tester@test.com",
                NormalizedEmail = "TESTER@TEST.COM"
            };

            Review olderReview = new Review
            {
                Id = 1,
                Title = "Older Review",
                Body = "This is an older valid review body.",
                Rating = 7,
                GameId = 1,
                Game = game,
                UserId = "user-1",
                User = user,
                CreatedOn = new DateTime(2024, 1, 1)
            };

            Review newerReview = new Review
            {
                Id = 2,
                Title = "Newer Review",
                Body = "This is a newer valid review body.",
                Rating = 9,
                GameId = 1,
                Game = game,
                UserId = "user-1",
                User = user,
                CreatedOn = new DateTime(2024, 2, 1)
            };

            dbContext.Games.Add(game);
            dbContext.Users.Add(user);
            dbContext.Reviews.AddRange(olderReview, newerReview);

            await dbContext.SaveChangesAsync();

            List<ReviewListItemViewModel> result = await reviewService.GetAllReviewsAsync();

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].ReviewId, Is.EqualTo(2));
            Assert.That(result[0].ReviewTitle, Is.EqualTo("Newer Review"));
            Assert.That(result[1].ReviewId, Is.EqualTo(1));
            Assert.That(result[1].ReviewTitle, Is.EqualTo("Older Review"));
        }
    }


}
