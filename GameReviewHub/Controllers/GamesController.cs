using GameReviewHub.Data;
using GameReviewHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameReviewHub.Controllers
{
    public class GamesController : Controller
    {
        private readonly GameReviewHubDbContext dbContext;

        public GamesController(GameReviewHubDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            Game[] allGames = dbContext
                .Games
                .AsNoTracking()
                .ToArray();

            return View(allGames);
        }
    }
}
