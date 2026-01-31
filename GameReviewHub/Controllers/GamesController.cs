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
            Game[] allGames = dbContext //Add .Take later
                .Games
                .AsNoTracking()
                .OrderBy(g => g.Title)
                .ToArray();

            return View(allGames);
        }

        [HttpGet] 
        public IActionResult Details(int id)
        {
            // SEO-friendly slugs could be added as a future improvement. Example: Games/Hades/Details

            Game? game = dbContext
                .Games
                .Include(g => g.GameGenres)
                .ThenInclude(gg => gg.Genre)
                .Include(g => g.Reviews)
                .AsSplitQuery()
                .AsNoTracking()
                .FirstOrDefault(g => g.Id == id);

            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }
    }
}
