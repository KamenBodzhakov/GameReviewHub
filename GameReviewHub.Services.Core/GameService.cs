using GameReviewHub.Data;
using GameReviewHub.Data.Models;
using GameReviewHub.Services.Core.Interfaces;
using GameReviewHub.ViewModels.Game;
using Microsoft.EntityFrameworkCore;

namespace GameReviewHub.Services.Core
{
    using static Common.ValidationConstants.Game;

    public class GameService : IGameService
    {
        private readonly GameReviewHubDbContext dbContext;

        public GameService(GameReviewHubDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<GameListItemViewModel>> ShowAllGamesAsync()
        {
            return await dbContext.Games
            .AsNoTracking()
            .OrderBy(g => g.Title)
            .ThenBy(g => g.ReleaseDate)
            .Select(g => new GameListItemViewModel
            {
                Id = g.Id,
                Title = g.Title,
                Developer = g.Developer,
                ReleaseDate = g.ReleaseDate,
                ShortDescription = g.Description.Length > 200
                    ? g.Description.Substring(0, GameCardMaxDescriptionLength) + "..."
                    : g.Description,
                AverageRating = g.Reviews.Any()
                    ? g.Reviews.Average(r => r.Rating)
                    : 0.0
            })
            .ToListAsync();
        }

        public async Task<GameDetailsViewModel?> GetGameDetailsAsync(int gameId)
        {
            return await dbContext
                .Games
                .AsNoTracking()
                .Where(g => g.Id == gameId)
                .Select(g => new GameDetailsViewModel
                {
                    Id = g.Id,
                    Title = g.Title,
                    Developer = g.Developer,
                    ReleaseDate = g.ReleaseDate,
                    Description = g.Description,
                    Genres = g.GameGenres
                        .Select(gg => gg.Genre.Name)
                        .OrderBy(name => name)
                        .ToList()
                })
                .FirstOrDefaultAsync();
        }
    }
}
