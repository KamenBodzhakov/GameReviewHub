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
        private readonly GameReviewHubDbContext dbContext;
        private readonly IReviewService reviewService;
        public ReviewsController(GameReviewHubDbContext dbContext, IReviewService reviewService)
        {
            this.dbContext = dbContext;
            this.reviewService = reviewService;
        }


        [HttpGet("Reviews/ByGame/{gameId:int}")] // Route: /Reviews/ByGame/1 instead of /Reviews/ByGame?gameId=1 , for better RESTful design
        public IActionResult ByGame(int gameId)
        {
            if (gameId <= 0) return BadRequest();
            Game? game = reviewService.GetGameWithReviews(gameId);
            if (game == null) return NotFound();

            return View(game);
        }


        [HttpGet]
        public IActionResult CreateReview(int gameId)
        {
            if (gameId <= 0) return BadRequest();

            CreateReviewViewModel? viewModel = reviewService.BuildCreateReviewViewModel(gameId);

            if (viewModel == null) return NotFound();

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
                    GameId = r.GameId,
                    ReviewTitle = r.Game.Title,
                    GameTitle = r.Title,
                    Rating = r.Rating,
                    CreatedOn = r.CreatedOn
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

            return RedirectToAction(nameof(ByGame), new { gameId = viewModel.GameId });
        }


        [HttpGet]
        public IActionResult EditReview(int gameId, int reviewId)
        {
            if (gameId <= 0 || reviewId <= 0)
            {
                return BadRequest();
            }

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

            EditReviewViewModel? editReviewViewModel = dbContext.Reviews
                .Where(r => r.Id == reviewId && r.GameId == gameId)
                .AsNoTracking()
                .Select(r => new EditReviewViewModel
                {
                    ReviewId = r.Id,
                    GameId = r.GameId,
                    GameTitle = r.Game.Title,
                    Input = new CreateReviewInputModel
                    {
                        Title = r.Title,
                        Body = r.Body,
                        Rating = r.Rating,
                    }
                })
                .FirstOrDefault();

            if (editReviewViewModel == null)
            {
                return NotFound();
            }

            return View(editReviewViewModel);
        }

        [HttpPost]
        public IActionResult EditReview(int gameId, int reviewId, CreateReviewInputModel input)
        {
            if (gameId <= 0 || reviewId <= 0)
            {
                return BadRequest();
            }

            Review? review = dbContext.Reviews
                .FirstOrDefault(r => r.Id == reviewId && r.GameId == gameId);

            if (review == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                var gameTitle = dbContext.Games
                    .Where(g => g.Id == gameId)
                    .Select(g => g.Title)
                    .FirstOrDefault();

                if (gameTitle == null)
                {
                    return NotFound();
                }

                EditReviewViewModel viewModel = new EditReviewViewModel()
                {
                    ReviewId = review.Id,
                    GameId = review.GameId,
                    GameTitle = gameTitle,
                    Input = input
                };


                return View(viewModel);
            }

            review.Title = input.Title;
            review.Body = input.Body;
            review.Rating = input.Rating;

            dbContext.SaveChanges();

            return RedirectToAction(nameof(ByGame), new { gameId = review.GameId });
        }
    }
}

