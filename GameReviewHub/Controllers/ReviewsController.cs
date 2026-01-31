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
    }
}
