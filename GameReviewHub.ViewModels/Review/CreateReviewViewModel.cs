namespace GameReviewHub.ViewModels.Review
{
    public class CreateReviewViewModel
    {
        public int GameId { get; set; }
        public string GameTitle { get; set; } = null!;

        public CreateReviewInputModel Input { get; set; } = new();
    }
}
