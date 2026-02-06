using GameReviewHub.Models.InputModels;

namespace GameReviewHub.Models.ViewModels.Review
{
    public class EditReviewViewModel
    {
        public int GameId { get; set; }
        public int ReviewId { get; set; }
        public string GameTitle { get; set; } = null!;
        public CreateReviewInputModel Input { get; set; } = new();
    }
}
