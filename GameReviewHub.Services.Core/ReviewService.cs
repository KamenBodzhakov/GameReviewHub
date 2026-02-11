using GameReviewHub.Data;
using GameReviewHub.Data.Models;
using GameReviewHub.Services.Core.Interfaces;
using GameReviewHub.ViewModels.Review;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var game = dbContext.Games
                .Where(g => g.Id == gameId)
                .Select(g => new { g.Id, g.Title })
                .FirstOrDefault();

            if (game == null) return null;

            return new CreateReviewViewModel
            {
                GameId = game.Id,
                GameTitle = game.Title
            };
        }
    }
}
