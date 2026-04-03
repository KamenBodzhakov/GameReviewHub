using GameReviewHub.Services.Core.Interfaces;
using GameReviewHub.ViewModels.Game;
using Microsoft.AspNetCore.Mvc;

namespace GameReviewHub.Controllers
{
    public class GamesController : Controller
    {

        private readonly IGameService gameService;

        public GamesController(IGameService gameService)
        {
            this.gameService = gameService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? searchTerm, int? genreId)
        {
            IEnumerable<GameListItemViewModel> games =
                await gameService.ShowAllGamesAsync(searchTerm, genreId);
            searchTerm = searchTerm?.Trim();

            ViewData["SearchQuery"] = searchTerm;
            ViewData["GenreId"] = genreId;
            ViewData["Genres"] = await gameService.GetAllGenreOptionsAsync();

            return View(games);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {

            if (id <= 0) return BadRequest();  // Slugs could be added as a future improvement. Example: Games/Hades/Details

            GameDetailsViewModel? viewModel = await gameService.GetGameDetailsAsync(id);
            if (viewModel == null) return NotFound();

            return View(viewModel);
        }
    }
}
