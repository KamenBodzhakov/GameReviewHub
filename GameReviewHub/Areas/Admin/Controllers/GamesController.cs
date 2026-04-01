using GameReviewHub.Services.Core.Interfaces;
using GameReviewHub.ViewModels.Game;
using GameReviewHub.ViewModels.Game.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameReviewHub.Areas.Admin.Controllers
{
    using static GameReviewHub.Common.ErrorMessages;
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class GamesController : Controller
    {     
        private readonly IGameService gameService;

        public GamesController(IGameService gameService)
        {
            this.gameService = gameService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<GameListItemViewModel> games = await gameService.ShowAllGamesAsync();

            return View(games);
        }

        public async Task<IActionResult> CreateGame()
        {
            CreateGameViewModel viewModel = await gameService.BuildCreateGameViewModelAsync();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateGame(CreateGameViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.AvailableGenres = await gameService.GetAllGenreOptionsAsync(); return View(model);
            }

            bool success = await gameService.CreateGameAsync(model.Input);

            if (!success)
            {
                ModelState.AddModelError(string.Empty, GameCreationFailed);
                model.AvailableGenres = await gameService.GetAllGenreOptionsAsync(); return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> EditGame(int id)
        {
            EditGameViewModel? viewModel = await gameService.BuildEditGameViewModelAsync(id);

            if (viewModel == null) return NotFound();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditGame(EditGameViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.AvailableGenres = await gameService.GetAllGenreOptionsAsync();
                return View(model);
            }

            bool success = await gameService.ConfirmEditGameAsync(model.GameId, model.Input);

            if (!success)
            {
                ModelState.AddModelError(string.Empty, GameEditingFailed);
                model.AvailableGenres = await gameService.GetAllGenreOptionsAsync();
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteGame(int id)
        {
            DeleteGameViewModel? viewModel = await gameService.BuildDeleteGameViewModelAsync(id);

            if (viewModel == null) return NotFound();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteGame(DeleteGameViewModel model)
        {
            bool success = await gameService.DeleteGameAsync(model.GameId);

            if (!success) return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
