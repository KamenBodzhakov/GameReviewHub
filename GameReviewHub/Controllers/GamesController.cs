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
        public async Task<IActionResult> Index(AllGamesQueryModel queryModel)
        {
            if (queryModel.CurrentPage < 1)
            {
                queryModel.CurrentPage = 1;
            }

            AllGamesPagedServiceModel pagedResult = await gameService
                .GetPagedGamesAsync(queryModel.SearchTerm,queryModel.GenreId,queryModel.CurrentPage,AllGamesQueryModel.GamesPerPage);

            queryModel.Games = pagedResult.Games;
            queryModel.TotalGamesCount = pagedResult.TotalGamesCount;

            ViewData["Genres"] = await gameService.GetAllGenreOptionsAsync();
            ViewData["SearchQuery"] = queryModel.SearchTerm;
            ViewData["GenreId"] = queryModel.GenreId;

            return View(queryModel);
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
