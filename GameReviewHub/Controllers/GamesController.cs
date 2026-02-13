
using GameReviewHub.Services.Core;
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
        public IActionResult Index()
        {
            IEnumerable<GameListItemViewModel> allGames = gameService.ShowAllGames();

            return View(allGames);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            if (id <= 0) return NotFound();  // Slugs could be added as a future improvement. Example: Games/Hades/Details

            GameDetailsViewModel? viewModel = gameService.GetGameDetails(id);
            if (viewModel == null) return NotFound();

            return View(viewModel);
        }
    }
}
