using System.ComponentModel.DataAnnotations.Schema;

namespace GameReviewHub.Models
{
    public class GameGenre
    {
        [ForeignKey(nameof(Game))]
        public int GameId { get; set; }
        public Game Game { get; set; } = null!;

        [ForeignKey(nameof(Genre))]
        public int GenreId { get; set; }
        public Genre Genre { get; set; } = null!;

    }
}
