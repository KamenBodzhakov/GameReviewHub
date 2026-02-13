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

        public async Task<Game?> GetGameWithReviewsAsync(int gameId)
        {
            return await dbContext.Games
                .Include(g => g.Reviews)
                .AsSplitQuery()
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Id == gameId);
        }

        public async Task<CreateReviewViewModel?> BuildCreateReviewViewModelAsync(int gameId)
        {
            CreateReviewViewModel? viewModel = await dbContext.Games
                .Where(g => g.Id == gameId)
                .Select(g => new CreateReviewViewModel
                {
                    GameId = g.Id,
                    GameTitle = g.Title
                })
                .FirstOrDefaultAsync();

            return viewModel;
        }


        public async Task<bool> CreateReviewAsync(int gameId, CreateReviewInputModel input)
        {
            bool gameExists = await dbContext.Games.AnyAsync(g => g.Id == gameId);

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
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<DeleteReviewViewModel?> BuildDeleteReviewViewModelAsync(int gameId, int reviewId)
        {
            DeleteReviewViewModel? viewModel = await dbContext.Reviews
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
                .FirstOrDefaultAsync();

            return viewModel;
        }

        public async Task<bool> DeleteReviewAsync(int gameId, int reviewId)
        {
            Review? review = await dbContext.Reviews
                .FirstOrDefaultAsync(r => r.Id == reviewId && r.GameId == gameId);

            if (review == null) return false;

            dbContext.Reviews.Remove(review);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<EditReviewViewModel?> BuildEditReviewViewModelAsync(int gameId, int reviewId)
        {
            EditReviewViewModel? viewModel = await dbContext.Reviews
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
                .FirstOrDefaultAsync();

            return viewModel;
        }

        public async Task<bool> ConfirmEditReviewAsync(int gameId, int reviewId, CreateReviewInputModel input)
        {
            Review? review = await dbContext.Reviews
                .FirstOrDefaultAsync(r => r.Id == reviewId && r.GameId == gameId);

            if (review == null) return false;

            review.Title = input.Title;
            review.Body = input.Body;
            review.Rating = input.Rating;

            await dbContext.SaveChangesAsync();

            return true;
        }
    }
}
