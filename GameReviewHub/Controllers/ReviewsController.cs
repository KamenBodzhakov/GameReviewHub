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
                Rating = input.Score,
                CreatedOn = DateTime.UtcNow
            };

            dbContext.Reviews.Add(review);
            dbContext.SaveChanges();

            return RedirectToAction(nameof(ByGame), new { gameId = game.Id } );
        }
    }
}
