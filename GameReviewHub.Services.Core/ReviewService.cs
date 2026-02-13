using GameReviewHub.Data;
using GameReviewHub.Data.Models;
using GameReviewHub.Services.Core.Interfaces;
using GameReviewHub.ViewModels;
using GameReviewHub.ViewModels.Review;
using Microsoft.EntityFrameworkCore;

namespace GameReviewHub.Services.Core
{
    public class ReviewService : IReviewService
    {
        private readonly GameReviewHubDbContext dbContext;

        public ReviewService(GameReviewHubDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Game? GetGameWithReviews(int gameId)
        {
            return dbContext.Games
                .Include(g => g.Reviews)
                .AsSplitQuery()
                .AsNoTracking()
                .FirstOrDefault(g => g.Id == gameId);
        }

        public CreateReviewViewModel? BuildCreateReviewViewModel(int gameId)
        {
            CreateReviewViewModel? viewModel = dbContext.Games
                .Where(g => g.Id == gameId)
                .Select(g => new CreateReviewViewModel
                {
                    GameId = g.Id,
                    GameTitle = g.Title
                })
                .FirstOrDefault();

            return viewModel;
        }


        public bool CreateReview(int gameId, CreateReviewInputModel input)
        {
            bool gameExists = dbContext.Games.Any(g => g.Id == gameId);

            if (!gameExists) return false;

            Review review = new Review
            {
                Title = input.Title,
                Body = input.Body,
                Rating = input.Rating,
                GameId = gameId,
                CreatedOn = DateTime.UtcNow
            };

            dbContext.Reviews.Add(review);
            dbContext.SaveChanges();

            return true;
        }

        public DeleteReviewViewModel? BuildDeleteReviewViewModel(int gameId, int reviewId)
        {
            DeleteReviewViewModel? viewModel = dbContext.Reviews
                .AsNoTracking()
                .Where(r => r.Id == reviewId && r.GameId == gameId)
                .Select(r => new DeleteReviewViewModel
                {
                    ReviewId = r.Id,
                    GameId = r.GameId,
                    ReviewTitle = r.Game.Title,
                    GameTitle = r.Title,
                    Rating = r.Rating,
                    CreatedOn = r.CreatedOn
                })
                .FirstOrDefault();

            return viewModel;
        }

        public bool DeleteReview(int gameId, int reviewId)
        {
            Review? review = dbContext.Reviews
                .FirstOrDefault(r => r.Id == reviewId && r.GameId == gameId);

            if (review == null) return false;

            dbContext.Reviews.Remove(review);
            dbContext.SaveChanges();

            return true;
        }

        public EditReviewViewModel? BuildEditReviewViewModel(int gameId, int reviewId)
        {
            EditReviewViewModel? viewModel = dbContext.Reviews
                .AsNoTracking()
                .Where(r => r.Id == reviewId && r.GameId == gameId)
                .Select(r => new EditReviewViewModel
                {
                    ReviewId = r.Id,
                    GameId = r.GameId,
                    GameTitle = r.Game.Title,
                    Input = new CreateReviewInputModel
                    {
                        Title = r.Title,
                        Body = r.Body,
                        Rating = r.Rating
                    }
                })
                .FirstOrDefault();

            return viewModel;
        }

        public bool ConfirmEditReview(int gameId, int reviewId, CreateReviewInputModel input)
        {
            Review? review = dbContext.Reviews
                .FirstOrDefault(r => r.Id == reviewId && r.GameId == gameId);

            if (review == null) return false;

            review.Title = input.Title;
            review.Body = input.Body;
            review.Rating = input.Rating;

            dbContext.SaveChanges();

            return true;
        }
    }
}
