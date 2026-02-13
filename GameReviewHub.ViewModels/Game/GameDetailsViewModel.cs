using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameReviewHub.ViewModels.Game
{
    public class GameDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Developer { get; set; } = null!;
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; } = null!;
        public IReadOnlyCollection<string> Genres { get; set; } = new HashSet<string>();
        public string? ImagePath { get; set; }
    }
}
