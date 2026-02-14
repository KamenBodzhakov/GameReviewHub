using GameReviewHub.Data.Models;
using GameReviewHub.Services.Core.Interfaces;
using GameReviewHub.ViewModels;
using GameReviewHub.ViewModels.Review;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GameReviewHub.Controllers
{
    [Authorize]
    public class ReviewsController : Controller
    {
        private readonly IReviewService reviewService;
        public ReviewsController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        [AllowAnonymous]
        [HttpGet("Reviews/ByGame/{gameId:int}")] // Route: /Reviews/ByGame/1 instead of /Reviews/ByGame?gameId=1 , for better RESTful design
        public async Task<IActionResult> ByGame(int gameId)
        {
            if (GameIdIsInvalid(gameId)) return BadRequest();

            Game? game = await reviewService.GetGameWithReviewsAsync(gameId);
            if (game == null) return NotFound();

            return View(game);
        }

        [HttpGet]
        public async Task<IActionResult> CreateReview(int gameId)
        {
            if (GameIdIsInvalid(gameId)) return BadRequest();

            CreateReviewViewModel? viewModel = await reviewService.BuildCreateReviewViewModelAsync(gameId);
            if (viewModel == null) return NotFound();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview(int gameId, CreateReviewInputModel input)
        {
            if (GameIdIsInvalid(gameId)) return BadRequest();

            CreateReviewViewModel? viewModel = await reviewService.BuildCreateReviewViewModelAsync(gameId);
            if (viewModel == null) return NotFound();

            if (!ModelState.IsValid)
            {
                viewModel.Input = input;
                return View(viewModel);
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            bool isCreated = await reviewService.CreateReviewAsync(gameId, input, userId);
            if (!isCreated) return NotFound();

            return RedirectToAction(nameof(ByGame), new { gameId });
        }


        [HttpGet]
        public async Task<IActionResult> DeleteReview(int gameId, int reviewId)
        {
            if (GameIdOrReviewIdIsInvalid(gameId, reviewId)) return BadRequest();

            DeleteReviewViewModel? viewModel = await reviewService.BuildDeleteReviewViewModelAsync(gameId, reviewId);
            if (viewModel == null) return NotFound();

            return View(viewModel);
        }


        //[HttpPost]
        //[ActionName(nameof(DeleteReview))]
        //public IActionResult DeleteReviewPost(int gameId, int reviewId) 
        //{
        //    if (gameId <= 0 || reviewId <= 0) return BadRequest();

        //    bool isDeleted = reviewService.DeleteReview(gameId, reviewId);
        //    if (!isDeleted) return NotFound();

        //    return RedirectToAction(nameof(ByGame), new { gameId });
        //}


        [HttpPost]
        public async Task<IActionResult> DeleteReview(DeleteReviewViewModel viewModel)
        {
            if (viewModel.GameId <= 0 || viewModel.ReviewId <= 0) return BadRequest();

            bool isDeleted = await reviewService.DeleteReviewAsync(viewModel.GameId, viewModel.ReviewId);
            if (!isDeleted) return NotFound();

            return RedirectToAction(nameof(ByGame), new { gameId = viewModel.GameId });
        }


        [HttpGet]
        public async Task<IActionResult> EditReview(int gameId, int reviewId)
        {
            if (GameIdOrReviewIdIsInvalid(gameId, reviewId)) return BadRequest();

            EditReviewViewModel? editReviewViewModel = await reviewService.BuildEditReviewViewModelAsync(gameId, reviewId);
            if (editReviewViewModel == null) return NotFound();

            return View(editReviewViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> EditReview(int gameId, int reviewId, EditReviewViewModel model)
        {
            if (GameIdOrReviewIdIsInvalid(gameId, reviewId)) return BadRequest();

            EditReviewViewModel? editReviewViewModel = await reviewService.BuildEditReviewViewModelAsync(gameId, reviewId);
            if (editReviewViewModel == null) return NotFound();

            if (!ModelState.IsValid)
            {
                editReviewViewModel.Input = model.Input;
                return View(editReviewViewModel);
            }

            bool isEditReviewConfirmed = await reviewService.ConfirmEditReviewAsync(model.GameId, model.ReviewId, model.Input);
            if (!isEditReviewConfirmed) return NotFound();

            return RedirectToAction(nameof(ByGame), new { gameId = model.GameId });
        }


        private static bool GameIdIsInvalid(int gameId) => gameId <= 0;
        private static bool GameIdOrReviewIdIsInvalid(int gameId, int reviewId) => gameId <= 0 || reviewId <= 0;
    }
}

