using GameReviewHub.Common;
using GameReviewHub.Services.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GameReviewHub.Controllers
{
    [Authorize]
    public class ReviewVotesController : Controller
    {
        private readonly IReviewVoteService reviewVoteService;

        public ReviewVotesController(IReviewVoteService reviewVoteService)
        {
            this.reviewVoteService = reviewVoteService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Vote(int reviewId, bool isUpvote, int gameId, string? returnUrl)
        {
            if (reviewId <= 0 || gameId <= 0)
            {
                return BadRequest();
            }

            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Unauthorized();
            }

            try
            {
                bool result = await reviewVoteService.VoteAsync(reviewId, userId, isUpvote);

                if (!result)
                {
                    TempData["VoteMessage"] = ErrorMessages.VoteAlreadySubmitted;
                }

                if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("ByGame", "Reviews", new { gameId });
            }
            catch (Exception)
            {
                TempData["VoteMessage"] = ErrorMessages.VoteUnexpectedError;

                if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("ByGame", "Reviews", new { gameId });
            }
        }
    }
}