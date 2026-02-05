using GameReviewHub.Data;
using GameReviewHub.Models.EntityModels;
using GameReviewHub.Models.InputModels;
using GameReviewHub.Models.ViewModels.Review;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameReviewHub.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly GameReviewHubDbContext dbContext;

        public ReviewsController(GameReviewHubDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet("Reviews/ByGame/{gameId:int}")] // Route: /Reviews/ByGame/1 instead of /Reviews/ByGame?gameId=1 , for better RESTful design
        public IActionResult ByGame(int gameId)
        {
            if (gameId <= 0)
            {
                return NotFound();
            }

            Game? game = dbContext
                .Games
                .Include(g => g.Reviews)
                .AsSplitQuery()
                .AsNoTracking()
                .FirstOrDefault(g => g.Id == gameId);

            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }


        [HttpGet]
        public IActionResult CreateReview(int gameId)
        {
            Game? game = dbContext.Games
                .Where(g => g.Id == gameId)
                .Select(g => new Game
                {
                    Id = g.Id,
                    Title = g.Title
                })
                .FirstOrDefault();

            if (game == null)
            {
                return NotFound();
            }

            CreateReviewViewModel viewModel = new CreateReviewViewModel()
            {
                GameId = game.Id,
                GameTitle = game.Title
            };

            return View(viewModel);
        }


        [HttpPost]
        public IActionResult CreateReview(int gameId, CreateReviewInputModel input)
        {
            Game? game = dbContext.Games
                .Where(g => g.Id == gameId)
                .Select(g => new Game
                {
                    Id = g.Id,
                    Title = g.Title
                })
                .FirstOrDefault();

            if (game == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                CreateReviewViewModel viewModel = new CreateReviewViewModel()
                {
                    GameId = game.Id,
                    GameTitle = game.Title,
                    Input = input
                };

                return View(viewModel);
            }

            Review review = new Review()
            {
                GameId = game.Id,
                Title = input.Title,
                Body = input.Body,
                Rating = input.Rating,
                CreatedOn = DateTime.UtcNow
            };

            dbContext.Reviews.Add(review);
            dbContext.SaveChanges();

            return RedirectToAction(nameof(ByGame), new { gameId = game.Id });
        }


        [HttpGet]
        public IActionResult DeleteReview(int gameId, int reviewId)
        {
            DeleteReviewViewModel? viewModel = dbContext.Reviews
                .AsNoTracking()
                .Where(r => r.Id == reviewId && r.GameId == gameId)
                .Select(r => new DeleteReviewViewModel
                {
                    ReviewId = r.Id,
                    GameTitle = r.Title,
                    GameId = r.Id
                })
                .FirstOrDefault();

            if (viewModel == null) return NotFound();

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult DeleteReview(DeleteReviewViewModel viewModel)
        {
            Review? review = dbContext.Reviews
                .FirstOrDefault(r => r.Id == viewModel.ReviewId && r.GameId == viewModel.GameId);

            if (review == null) return NotFound();

            dbContext.Reviews.Remove(review);
            dbContext.SaveChanges();

            return RedirectToAction(nameof(ByGame), new { gameId = viewModel.GameId } );
        }
    }
}
