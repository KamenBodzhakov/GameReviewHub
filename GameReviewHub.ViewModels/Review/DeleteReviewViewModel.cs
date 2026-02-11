namespace GameReviewHub.ViewModels.Review
{
    public class DeleteReviewViewModel
    {
        public int GameId { get; set; }
        public int ReviewId { get; set; }
        public string GameTitle { get; set; } = null!;
        public string ReviewTitle { get; set; } = null!;
        public int Rating { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
