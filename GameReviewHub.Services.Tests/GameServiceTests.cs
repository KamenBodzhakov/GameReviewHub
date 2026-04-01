using GameReviewHub.Data;
using GameReviewHub.Data.Models;
using GameReviewHub.Services.Core;
using GameReviewHub.Services.Tests.Helpers;
using GameReviewHub.ViewModels.Game;
using GameReviewHub.ViewModels.Game.Admin;
using Microsoft.EntityFrameworkCore;

namespace GameReviewHub.Tests.Services
{
    [TestFixture]
    public class GameServiceTests
    {
        private GameReviewHubDbContext dbContext = null!;
        private GameService gameService = null!;

        [SetUp]
        public void SetUp()
        {
            string databaseName = Guid.NewGuid().ToString();

            dbContext = DbContextMockHelper.CreateInMemoryDbContext(databaseName);
            gameService = new GameService(dbContext);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        [Test]
        public async Task ShowAllGamesAsync_ShouldReturnAllGamesOrderedByTitleThenReleaseDate()
        {
            dbContext.Games.AddRange(new List<Game>
    {
        new Game
        {
            Id = 1,
            Title = "Zelda",
            Developer = "Nintendo",
            Description = "A valid long enough description for Zelda.",
            ReleaseDate = new DateTime(2023, 1, 1),
            ImagePath = "/images/games/zelda.jpg"
        },
        new Game
        {
            Id = 2,
            Title = "Baldur's Gate 3",
            Developer = "Larian Studios",
            Description = "A valid long enough description for Baldurs Gate 3.",
            ReleaseDate = new DateTime(2024, 1, 1),
            ImagePath = "/images/games/bg3.jpg"
        },
        new Game
        {
            Id = 3,
            Title = "Baldur's Gate 3",
            Developer = "Larian Studios",
            Description = "Another valid long enough description for Baldurs Gate 3.",
            ReleaseDate = new DateTime(2022, 1, 1),
            ImagePath = "/images/games/bg3-old.jpg"
        }
    });

            await dbContext.SaveChangesAsync();

            IEnumerable<GameListItemViewModel> result = await gameService.ShowAllGamesAsync();

            List<GameListItemViewModel> games = result.ToList();

            Assert.That(games.Count, Is.EqualTo(3));
            Assert.That(games[0].Title, Is.EqualTo("Baldur's Gate 3"));
            Assert.That(games[0].ReleaseDate, Is.EqualTo(new DateTime(2022, 1, 1)));
            Assert.That(games[1].Title, Is.EqualTo("Baldur's Gate 3"));
            Assert.That(games[1].ReleaseDate, Is.EqualTo(new DateTime(2024, 1, 1)));
            Assert.That(games[2].Title, Is.EqualTo("Zelda"));
        }

        [Test]
        public async Task GetGameDetailsAsync_ShouldReturnNull_WhenGameDoesNotExist()
        {
            GameDetailsViewModel? result = await gameService.GetGameDetailsAsync(999);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetGameDetailsAsync_ShouldReturnCorrectGameDetails()
        {
            Genre actionGenre = new Genre
            {
                Id = 1,
                Name = "Action"
            };

            Genre adventureGenre = new Genre
            {
                Id = 2,
                Name = "Adventure"
            };

            Game game = new Game
            {
                Id = 1,
                Title = "Hades",
                Developer = "Supergiant Games",
                Description = "A valid long enough description for Hades.",
                ReleaseDate = new DateTime(2020, 9, 17),
                ImagePath = "/images/games/hades.jpg"
            };

            GameGenre firstGameGenre = new GameGenre
            {
                GameId = game.Id,
                Game = game,
                GenreId = actionGenre.Id,
                Genre = actionGenre
            };

            GameGenre secondGameGenre = new GameGenre
            {
                GameId = game.Id,
                Game = game,
                GenreId = adventureGenre.Id,
                Genre = adventureGenre
            };

            game.GameGenres.Add(firstGameGenre);
            game.GameGenres.Add(secondGameGenre);

            dbContext.Games.Add(game);
            dbContext.Genres.AddRange(actionGenre, adventureGenre);
            dbContext.GamesGenres.AddRange(firstGameGenre, secondGameGenre);

            await dbContext.SaveChangesAsync();

            GameDetailsViewModel? result = await gameService.GetGameDetailsAsync(1);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Id, Is.EqualTo(1));
            Assert.That(result.Title, Is.EqualTo("Hades"));
            Assert.That(result.Developer, Is.EqualTo("Supergiant Games"));
            Assert.That(result.Description, Is.EqualTo("A valid long enough description for Hades."));
            Assert.That(result.ImagePath, Is.EqualTo("/images/games/hades.jpg"));
            Assert.That(result.Genres.Count, Is.EqualTo(2));
            Assert.That(result.Genres, Is.EqualTo(new[] { "Action", "Adventure" }));
        }

        [Test]
        public async Task BuildCreateGameViewModelAsync_ShouldReturnAllGenres()
        {
            dbContext.Genres.AddRange(new List<Genre>
    {
        new Genre { Id = 1, Name = "RPG" },
        new Genre { Id = 2, Name = "Action" },
        new Genre { Id = 3, Name = "Adventure" }
    });

            await dbContext.SaveChangesAsync();

            CreateGameViewModel result = await gameService.BuildCreateGameViewModelAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.AvailableGenres.Count(), Is.EqualTo(3));
            Assert.That(result.AvailableGenres.Any(g => g.Name == "RPG"), Is.True);
            Assert.That(result.AvailableGenres.Any(g => g.Name == "Action"), Is.True);
            Assert.That(result.AvailableGenres.Any(g => g.Name == "Adventure"), Is.True);
        }

        [Test]
        public async Task CreateGameAsync_ShouldCreateGameSuccessfully_WhenInputIsValid()
        {
            dbContext.Genres.AddRange(new List<Genre>
    {
        new Genre { Id = 1, Name = "Action" },
        new Genre { Id = 2, Name = "Adventure" }
    });

            await dbContext.SaveChangesAsync();

            CreateGameInputModel input = new CreateGameInputModel
            {
                Title = "Darkest Dungeon II",
                Developer = "Red Hook Studios",
                Description = "A valid long enough description for Darkest Dungeon II.",
                ReleaseDate = new DateTime(2023, 5, 8),
                ImagePath = "/images/games/darkest-dungeon-2.jpg",
                SelectedGenreIds = new List<int> { 1, 2 }
            };

            bool result = await gameService.CreateGameAsync(input);

            Game? createdGame = await dbContext.Games
                .Include(g => g.GameGenres)
                .FirstOrDefaultAsync(g => g.Title == "Darkest Dungeon II");

            Assert.That(result, Is.True);
            Assert.That(createdGame, Is.Not.Null);
            Assert.That(createdGame!.Developer, Is.EqualTo("Red Hook Studios"));
            Assert.That(createdGame.Description, Is.EqualTo("A valid long enough description for Darkest Dungeon II."));
            Assert.That(createdGame.ReleaseDate, Is.EqualTo(new DateTime(2023, 5, 8)));
            Assert.That(createdGame.ImagePath, Is.EqualTo("/images/games/darkest-dungeon-2.jpg"));
            Assert.That(createdGame.GameGenres.Count, Is.EqualTo(2));
            Assert.That(createdGame.GameGenres.Any(gg => gg.GenreId == 1), Is.True);
            Assert.That(createdGame.GameGenres.Any(gg => gg.GenreId == 2), Is.True);
        }

        [Test]
        public async Task CreateGameAsync_ShouldReturnFalse_WhenSelectedGenresAreInvalid()
        {
            dbContext.Genres.Add(new Genre { Id = 1, Name = "Action" });

            await dbContext.SaveChangesAsync();

            CreateGameInputModel input = new CreateGameInputModel
            {
                Title = "Test Game",
                Developer = "Test Studio",
                Description = "A valid long enough description for the test game.",
                ReleaseDate = new DateTime(2024, 1, 1),
                ImagePath = "/images/games/test-game.jpg",
                SelectedGenreIds = new List<int> { 1, 999 }
            };

            bool result = await gameService.CreateGameAsync(input);

            int gamesCount = await dbContext.Games.CountAsync();

            Assert.That(result, Is.False);
            Assert.That(gamesCount, Is.EqualTo(0));
        }

        [Test]
        public async Task BuildEditGameViewModelAsync_ShouldReturnNull_WhenGameDoesNotExist()
        {
            EditGameViewModel? result = await gameService.BuildEditGameViewModelAsync(999);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task BuildEditGameViewModelAsync_ShouldReturnCorrectViewModel_WhenGameExists()
        {
            Genre firstGenre = new Genre
            {
                Id = 1,
                Name = "Action"
            };

            Genre secondGenre = new Genre
            {
                Id = 2,
                Name = "Adventure"
            };

            Game game = new Game
            {
                Id = 1,
                Title = "Hades",
                Developer = "Supergiant Games",
                Description = "A valid long enough description for Hades.",
                ReleaseDate = new DateTime(2020, 9, 17),
                ImagePath = "/images/games/hades.jpg"
            };

            GameGenre firstGameGenre = new GameGenre
            {
                GameId = game.Id,
                Game = game,
                GenreId = firstGenre.Id,
                Genre = firstGenre
            };

            GameGenre secondGameGenre = new GameGenre
            {
                GameId = game.Id,
                Game = game,
                GenreId = secondGenre.Id,
                Genre = secondGenre
            };

            game.GameGenres.Add(firstGameGenre);
            game.GameGenres.Add(secondGameGenre);

            dbContext.Genres.AddRange(firstGenre, secondGenre);
            dbContext.Games.Add(game);
            dbContext.GamesGenres.AddRange(firstGameGenre, secondGameGenre);

            await dbContext.SaveChangesAsync();

            EditGameViewModel? result = await gameService.BuildEditGameViewModelAsync(1);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.GameId, Is.EqualTo(1));
            Assert.That(result.Input.Title, Is.EqualTo("Hades"));
            Assert.That(result.Input.Developer, Is.EqualTo("Supergiant Games"));
            Assert.That(result.Input.Description, Is.EqualTo("A valid long enough description for Hades."));
            Assert.That(result.Input.ReleaseDate, Is.EqualTo(new DateTime(2020, 9, 17)));
            Assert.That(result.Input.ImagePath, Is.EqualTo("/images/games/hades.jpg"));
            Assert.That(result.Input.SelectedGenreIds.Count, Is.EqualTo(2));
            Assert.That(result.Input.SelectedGenreIds.Contains(1), Is.True);
            Assert.That(result.Input.SelectedGenreIds.Contains(2), Is.True);
            Assert.That(result.AvailableGenres.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task ConfirmEditGameAsync_ShouldReturnFalse_WhenGameDoesNotExist()
        {
            CreateGameInputModel input = new CreateGameInputModel
            {
                Title = "Updated Game",
                Developer = "Updated Developer",
                Description = "A valid long enough updated description.",
                ReleaseDate = new DateTime(2024, 1, 1),
                ImagePath = "/images/games/updated-game.jpg",
                SelectedGenreIds = new List<int> { 1 }
            };

            bool result = await gameService.ConfirmEditGameAsync(999, input);

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task ConfirmEditGameAsync_ShouldUpdateGameSuccessfully_WhenInputIsValid()
        {
            Genre firstGenre = new Genre
            {
                Id = 1,
                Name = "Action"
            };

            Genre secondGenre = new Genre
            {
                Id = 2,
                Name = "Adventure"
            };

            Genre thirdGenre = new Genre
            {
                Id = 3,
                Name = "RPG"
            };

            Game game = new Game
            {
                Id = 1,
                Title = "Old Title",
                Developer = "Old Developer",
                Description = "A valid long enough old description.",
                ReleaseDate = new DateTime(2020, 1, 1),
                ImagePath = "/images/games/old.jpg"
            };

            GameGenre oldGameGenre = new GameGenre
            {
                GameId = game.Id,
                Game = game,
                GenreId = firstGenre.Id,
                Genre = firstGenre
            };

            game.GameGenres.Add(oldGameGenre);

            dbContext.Genres.AddRange(firstGenre, secondGenre, thirdGenre);
            dbContext.Games.Add(game);
            dbContext.GamesGenres.Add(oldGameGenre);

            await dbContext.SaveChangesAsync();

            CreateGameInputModel input = new CreateGameInputModel
            {
                Title = "New Title",
                Developer = "New Developer",
                Description = "A valid long enough new description.",
                ReleaseDate = new DateTime(2024, 5, 10),
                ImagePath = "/images/games/new.jpg",
                SelectedGenreIds = new List<int> { 2, 3 }
            };

            bool result = await gameService.ConfirmEditGameAsync(1, input);

            Game? updatedGame = await dbContext.Games
                .Include(g => g.GameGenres)
                .FirstOrDefaultAsync(g => g.Id == 1);

            Assert.That(result, Is.True);
            Assert.That(updatedGame, Is.Not.Null);
            Assert.That(updatedGame!.Title, Is.EqualTo("New Title"));
            Assert.That(updatedGame.Developer, Is.EqualTo("New Developer"));
            Assert.That(updatedGame.Description, Is.EqualTo("A valid long enough new description."));
            Assert.That(updatedGame.ReleaseDate, Is.EqualTo(new DateTime(2024, 5, 10)));
            Assert.That(updatedGame.ImagePath, Is.EqualTo("/images/games/new.jpg"));
            Assert.That(updatedGame.GameGenres.Count, Is.EqualTo(2));
            Assert.That(updatedGame.GameGenres.Any(gg => gg.GenreId == 2), Is.True);
            Assert.That(updatedGame.GameGenres.Any(gg => gg.GenreId == 3), Is.True);
        }

        [Test]
        public async Task BuildDeleteGameViewModelAsync_ShouldReturnNull_WhenGameDoesNotExist()
        {
            DeleteGameViewModel? result = await gameService.BuildDeleteGameViewModelAsync(999);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task BuildDeleteGameViewModelAsync_ShouldReturnCorrectViewModel_WhenGameExists()
        {
            Game game = new Game
            {
                Id = 1,
                Title = "Hades",
                Developer = "Supergiant Games",
                Description = "A valid long enough description.",
                ReleaseDate = new DateTime(2020, 9, 17),
                ImagePath = "/images/games/hades.jpg"
            };

            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();

            DeleteGameViewModel? result = await gameService.BuildDeleteGameViewModelAsync(1);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.GameId, Is.EqualTo(1));
            Assert.That(result.Title, Is.EqualTo("Hades"));
            Assert.That(result.Developer, Is.EqualTo("Supergiant Games"));
            Assert.That(result.ImagePath, Is.EqualTo("/images/games/hades.jpg"));
        }

        [Test]
        public async Task DeleteGameAsync_ShouldReturnFalse_WhenGameDoesNotExist()
        {
            bool result = await gameService.DeleteGameAsync(999);

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task DeleteGameAsync_ShouldDeleteGameSuccessfully()
        {
            Game game = new Game
            {
                Id = 1,
                Title = "Test Game",
                Developer = "Test Dev",
                Description = "A valid long enough description.",
                ReleaseDate = new DateTime(2022, 1, 1),
                ImagePath = "/images/games/test.jpg"
            };

            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();

            bool result = await gameService.DeleteGameAsync(1);

            int gamesCount = await dbContext.Games.CountAsync();

            Assert.That(result, Is.True);
            Assert.That(gamesCount, Is.EqualTo(0));
        }

        [Test]
        public async Task ConfirmEditGameAsync_ShouldUpdateGameSuccessfully()
        {
            var game = new Game
            {
                Title = "Old Title",
                Developer = "Old Dev",
                Description = "Old Description",
                ReleaseDate = new DateTime(2020, 1, 1)
            };

            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();

            var input = new CreateGameInputModel
            {
                Title = "New Title",
                Developer = "New Dev",
                Description = "New Description",
                ReleaseDate = new DateTime(2022, 1, 1),
                ImagePath = "/images/new.jpg"
            };

            bool result = await gameService.ConfirmEditGameAsync(game.Id, input);

            var updatedGame = await dbContext.Games.FindAsync(game.Id);

            Assert.That(result, Is.True);
            Assert.That(updatedGame!.Title, Is.EqualTo("New Title"));
            Assert.That(updatedGame.Developer, Is.EqualTo("New Dev"));
            Assert.That(updatedGame.Description, Is.EqualTo("New Description"));
            Assert.That(updatedGame.ReleaseDate, Is.EqualTo(new DateTime(2022, 1, 1)));
            Assert.That(updatedGame.ImagePath, Is.EqualTo("/images/new.jpg"));
        }
    }
}