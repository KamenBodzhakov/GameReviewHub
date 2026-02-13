using GameReviewHub.Data;
using GameReviewHub.Data.Models;
using GameReviewHub.Services.Core.Interfaces;
using GameReviewHub.ViewModels;
using GameReviewHub.ViewModels.Review;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameReviewHub.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly IReviewService reviewService;
        public ReviewsController(GameReviewHubDbContext dbContext, IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }


        [HttpGet("Reviews/ByGame/{gameId:int}")] // Route: /Reviews/ByGame/1 instead of /Reviews/ByGame?gameId=1 , for better RESTful design
        public IActionResult ByGame(int gameId)
        {
            if (GameIdIsInvalid(gameId)) return BadRequest();

            Game? game = reviewService.GetGameWithReviews(gameId);
            if (game == null) return NotFound();

            return View(game);
        }


        [HttpGet]
        public IActionResult CreateReview(int gameId)
        {
            if (GameIdIsInvalid(gameId)) return BadRequest();

            CreateReviewViewModel? viewModel = reviewService.BuildCreateReviewViewModel(gameId);
            if (viewModel == null) return NotFound();

            return View(viewModel);
        }


        [HttpPost]
        public IActionResult CreateReview(int gameId, CreateReviewInputModel input)
        {
            if (GameIdIsInvalid(gameId)) return BadRequest();

            CreateReviewViewModel? viewModel = reviewService.BuildCreateReviewViewModel(gameId);
            if (viewModel == null) return NotFound();

            if (!ModelState.IsValid)
            {
                viewModel.Input = input;
                return View(viewModel);
            }

            bool isCreated = reviewService.CreateReview(gameId, input);
            if (!isCreated) return NotFound();

            return RedirectToAction(nameof(ByGame), new { gameId });
        }


        [HttpGet]
        public IActionResult DeleteReview(int gameId, int reviewId)
        {
            if (GameIdOrReviewIdIsInvalid(gameId, reviewId)) return BadRequest();

            DeleteReviewViewModel? viewModel = reviewService.BuildDeleteReviewViewModel(gameId, reviewId);
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
        public IActionResult DeleteReview(DeleteReviewViewModel viewModel)
        {
            if (viewModel.GameId <= 0 || viewModel.ReviewId <= 0) return BadRequest();

            bool isDeleted = this.reviewService.DeleteReview(viewModel.GameId, viewModel.ReviewId);
            if (!isDeleted) return NotFound();

            return RedirectToAction(nameof(ByGame), new { gameId = viewModel.GameId });
        }


        [HttpGet]
        public IActionResult EditReview(int gameId, int reviewId)
        {
            if (GameIdOrReviewIdIsInvalid(gameId, reviewId)) return BadRequest();

            EditReviewViewModel? editReviewViewModel = reviewService.BuildEditReviewViewModel(gameId, reviewId);
            if (editReviewViewModel == null) return NotFound();

            return View(editReviewViewModel);
        }


        [HttpPost]
        public IActionResult EditReview(int gameId, int reviewId, EditReviewViewModel model)
        {
            if (GameIdOrReviewIdIsInvalid(gameId, reviewId)) return BadRequest();

            EditReviewViewModel? editReviewViewModel = reviewService.BuildEditReviewViewModel(gameId, reviewId);
            if (editReviewViewModel == null) return NotFound();

            if (!ModelState.IsValid)
            {
                editReviewViewModel.Input = model.Input;
                return View(editReviewViewModel);
            }

            bool isEditReviewConfirmed = reviewService.ConfirmEditReview(model.GameId, model.ReviewId, model.Input);
            if (!isEditReviewConfirmed) return NotFound();

            return RedirectToAction(nameof(ByGame), new { gameId = model.GameId });
        }


        private static bool GameIdIsInvalid(int gameId) => gameId <= 0;
        private static bool GameIdOrReviewIdIsInvalid(int gameId, int reviewId) => gameId <= 0 || reviewId <= 0;
    }
}

