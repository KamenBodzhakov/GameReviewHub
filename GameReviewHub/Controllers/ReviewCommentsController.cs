using GameReviewHub.Common;
using GameReviewHub.Services.Core.Interfaces;
using GameReviewHub.ViewModels.ReviewComment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GameReviewHub.Controllers
{
    [Authorize]
    public class ReviewCommentsController : Controller
    {
        private IReviewCommentService reviewCommentService;
        private IReviewService reviewService;

        public ReviewCommentsController(IReviewCommentService reviewCommentService, IReviewService reviewService)
        {
            this.reviewCommentService = reviewCommentService;
            this.reviewService = reviewService;
        }

        [HttpGet]
        public async Task<IActionResult> CreateReviewComment(int reviewId, int gameId)
        {
            if (reviewId <= 0 || gameId <= 0) return BadRequest();

            bool reviewExists = await reviewService.ReviewExistsAsync(reviewId);
            if (!reviewExists) return NotFound();

            CreateReviewCommentViewModel viewModel = new CreateReviewCommentViewModel
            {
                ReviewId = reviewId,
                GameId = gameId
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateReviewComment(CreateReviewCommentViewModel viewModel)
        {
            if (viewModel.ReviewId <= 0 || viewModel.GameId <= 0)
            {
                return BadRequest();
            }

            bool reviewExists = await reviewService.ReviewExistsAsync(viewModel.ReviewId);
            if (!reviewExists)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            try
            {
                bool isCreated = await reviewCommentService.AddCommentAsync(viewModel.ReviewId, viewModel.Input, userId);

                if (!isCreated)
                {
                    ModelState.AddModelError(string.Empty, ErrorMessages.CommentCreationFailed);
                    return View(viewModel);
                }

                return RedirectToAction("ByGame", "Reviews", new { gameId = viewModel.GameId });
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, ErrorMessages.UnexpectedError);
                return View(viewModel);
            }
        }


        //    if (!isCreated)
        //    {
        //        ModelState.AddModelError(string.Empty, "Failed to create comment. Please try again.");
        //        return View(viewModel);
        //    }
        //}
    }
}
