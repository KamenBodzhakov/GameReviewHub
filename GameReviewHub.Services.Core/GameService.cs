using GameReviewHub.Data;
using GameReviewHub.Data.Models;
using GameReviewHub.Services.Core.Interfaces;
using GameReviewHub.ViewModels.Game;
using GameReviewHub.ViewModels.Game.Admin;
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

        public async Task<IEnumerable<GameListItemViewModel>> ShowAllGamesAsync(string? searchTerm = null)
        {
            IQueryable<Game> query = dbContext.Games.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(g => g.Title.Contains(searchTerm));
            }

            return await query
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
                        : 0.0,
                    ImagePath = g.ImagePath
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
                    ImagePath = g.ImagePath,
                    Genres = g.GameGenres
                        .Select(gg => gg.Genre.Name)
                        .OrderBy(name => name)
                        .ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<CreateGameViewModel> BuildCreateGameViewModelAsync()
        {
            CreateGameViewModel viewModel = new CreateGameViewModel
            {
                Input = new CreateGameInputModel
                {
                    ReleaseDate = DateTime.Today
                },
                AvailableGenres = await GetAllGenreOptionsAsync()
            };

            return viewModel;
        }

        public async Task<bool> CreateGameAsync(CreateGameInputModel input)
        {
            List<int> selectedGenreIds = input.SelectedGenreIds
                .Distinct()
                .ToList();

            List<Genre> selectedGenres = await dbContext.Genres
                .Where(g => selectedGenreIds.Contains(g.Id))
                .ToListAsync();

            if (selectedGenres.Count != selectedGenreIds.Count) return false;

            Game game = new Game
            {
                Title = input.Title,
                Developer = input.Developer,
                Description = input.Description,
                ReleaseDate = input.ReleaseDate,
                ImagePath = input.ImagePath
            };

            foreach (Genre genre in selectedGenres)
            {
                game.GameGenres.Add(new GameGenre
                {
                    GenreId = genre.Id
                });
            }

            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<GameGenreOptionViewModel>> GetAllGenreOptionsAsync()
        {
            return await dbContext.Genres
                .AsNoTracking()
                .OrderBy(g => g.Name)
                .Select(g => new GameGenreOptionViewModel
                {
                    Id = g.Id,
                    Name = g.Name
                })
                .ToListAsync();
        }

        public async Task<EditGameViewModel?> BuildEditGameViewModelAsync(int gameId)
        {
            EditGameViewModel? viewModel = await dbContext.Games
                .AsNoTracking()
                .Where(g => g.Id == gameId)
                .Select(g => new EditGameViewModel
                {
                    GameId = g.Id,
                    Input = new CreateGameInputModel
                    {
                        Title = g.Title,
                        Developer = g.Developer,
                        Description = g.Description,
                        ReleaseDate = g.ReleaseDate,
                        ImagePath = g.ImagePath,
                        SelectedGenreIds = g.GameGenres
                            .Select(gg => gg.GenreId)
                            .ToList()
                    }
                })
                .FirstOrDefaultAsync();

            if (viewModel == null) return null;

            viewModel.AvailableGenres = await GetAllGenreOptionsAsync();

            return viewModel;
        }

        public async Task<bool> ConfirmEditGameAsync(int gameId, CreateGameInputModel input)
        {
            Game? game = await dbContext.Games
                .Include(g => g.GameGenres)
                .FirstOrDefaultAsync(g => g.Id == gameId);

            if (game == null) return false;

            List<int> selectedGenreIds = input.SelectedGenreIds
                .Distinct()
                .ToList();

            List<Genre> selectedGenres = await dbContext.Genres
                .Where(g => selectedGenreIds.Contains(g.Id))
                .ToListAsync();

            if (selectedGenres.Count != selectedGenreIds.Count) return false;

            game.Title = input.Title;
            game.Developer = input.Developer;
            game.Description = input.Description;
            game.ReleaseDate = input.ReleaseDate;
            game.ImagePath = input.ImagePath;

            game.GameGenres.Clear();

            foreach (Genre genre in selectedGenres)
            {
                game.GameGenres.Add(new GameGenre
                {
                    GenreId = genre.Id
                });
            }

            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<DeleteGameViewModel?> BuildDeleteGameViewModelAsync(int gameId)
        {
            DeleteGameViewModel? viewModel = await dbContext.Games
                .AsNoTracking()
                .Where(g => g.Id == gameId)
                .Select(g => new DeleteGameViewModel
                {
                    GameId = g.Id,
                    Title = g.Title,
                    Developer = g.Developer,
                    ImagePath = g.ImagePath
                })
                .FirstOrDefaultAsync();

            return viewModel;
        }

        public async Task<bool> DeleteGameAsync(int gameId)
        {
            Game? game = await dbContext.Games
                .Include(g => g.GameGenres)
                .Include(g => g.Reviews)
                .FirstOrDefaultAsync(g => g.Id == gameId);

            if (game == null) return false;

            dbContext.GamesGenres.RemoveRange(game.GameGenres);
            dbContext.Reviews.RemoveRange(game.Reviews);
            dbContext.Games.Remove(game);

            await dbContext.SaveChangesAsync();

            return true;
        }
    }
}

