namespace GameReviewHub.ViewModels.Game
{
    public class GameListItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;

        public string Developer { get; set; } = null!;

        public string ShortDescription { get; set; } = null!;

        public double AverageRating { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? ImagePath { get; set; }
    }
}
