namespace GameReviewHub.ViewModels.Game.Admin
{
    public class CreateGameViewModel
    {
        public CreateGameInputModel Input { get; set; } = new();

        public IEnumerable<GameGenreOptionViewModel> AvailableGenres { get; set; }
            = new List<GameGenreOptionViewModel>();
    }
}