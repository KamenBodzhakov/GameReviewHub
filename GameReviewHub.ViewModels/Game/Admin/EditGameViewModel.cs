using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameReviewHub.ViewModels.Game.Admin
{
    public class EditGameViewModel
    {
        public int GameId { get; set; }
        public CreateGameInputModel Input { get; set; } = new();
        public IEnumerable<GameGenreOptionViewModel> AvailableGenres { get; set; } = new List<GameGenreOptionViewModel>();
    }
}
