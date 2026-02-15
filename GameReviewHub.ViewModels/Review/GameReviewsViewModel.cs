namespace GameReviewHub.ViewModels.Review
{
    public class GameReviewsViewModel
    {
        public int GameId { get; set; }
        public string GameTitle { get; set; } = null!;
        public string GameImageUrl { get; set; } = null!;
        public IEnumerable<ReviewListItemViewModel> Reviews { get; set; } = new List<ReviewListItemViewModel>();
    }
}
