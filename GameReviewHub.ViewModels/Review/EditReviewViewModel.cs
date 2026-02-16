namespace GameReviewHub.ViewModels.Review
{
    public class EditReviewViewModel
    {
        public int GameId { get; set; }
        public int ReviewId { get; set; }
        public string? GameTitle { get; set; }
        public CreateReviewInputModel Input { get; set; } = new();
    }
}
