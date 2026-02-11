using GameReviewHub.Data;
using GameReviewHub.Data.Models;
using GameReviewHub.ViewModels.Game;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameReviewHub.Controllers
{
    using static Common.ValidationConstants.Game;
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
            IEnumerable<GameListItemViewModel> allGames = dbContext //Add .Take later
                .Games
                .AsNoTracking()
                .OrderBy(g => g.Title)
                .Select(g => new GameListItemViewModel
                {
                    Id = g.Id,
                    Title = g.Title,
                    Developer = g.Developer,
                    ReleaseDate = g.ReleaseDate,
                    ShortDescription = g.Description.Length > 200
                        ? g.Description.Substring(0, GameCardMaxDescriptionLength) + "..."
                        : g.Description,
                    AverageRating = g.Reviews.Any()
                        ? g.Reviews.Average(r => r.Rating)
                        : 0.0
                })
                .ToList();

            return View(allGames);
        }

        // [HttpGet("{id:int:min(1)}")]
        [HttpGet]
        public IActionResult Details(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

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
