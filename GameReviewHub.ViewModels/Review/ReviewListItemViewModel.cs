
namespace GameReviewHub.ViewModels.Review
{
    public class ReviewListItemViewModel
    {
        public int ReviewId { get; set; }
        public int GameId { get; set; }
        public string ReviewTitle { get; set; } = null!;
        public string GameTitle { get; set; } = null!;
        public string Body { get; set; } = null!;
        public int Rating { get; set; }
        public DateTime CreatedOn { get; set; }
        public string AuthorUserName { get; set; } = null!;
    }
}
