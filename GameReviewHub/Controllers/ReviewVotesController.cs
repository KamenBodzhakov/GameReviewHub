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
        public async Task<IActionResult> Vote(int reviewId, bool isUpvote, int gameId, string? returnUrl)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            bool result = await reviewVoteService.VoteAsync(reviewId, userId!, isUpvote);

            if (!result)
            {
                TempData["VoteMessage"] = "You have already voted for this review.";
            }

            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("ByGame", "Reviews", new { gameId });
        }

    }
}
