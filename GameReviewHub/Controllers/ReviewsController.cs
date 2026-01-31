using GameReviewHub.Data;
using GameReviewHub.Models;
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

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ByGame(int gameId)
        {
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
    }
}
