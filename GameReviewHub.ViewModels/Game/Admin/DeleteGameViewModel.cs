namespace GameReviewHub.ViewModels.Game.Admin
{
    public class DeleteGameViewModel
    {
        public int GameId { get; set; }

        public string Title { get; set; } = null!;

        public string? ImagePath { get; set; }

        public string Developer { get; set; } = null!;
    }
}