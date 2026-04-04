namespace GameReviewHub.ViewModels.Game
{
    public class AllGamesQueryModel
    {
        public const int GamesPerPage = 6;

        public string? SearchTerm { get; set; }

        public int? GenreId { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalGamesCount { get; set; }

        public int TotalPages =>
            (int)Math.Ceiling((double)TotalGamesCount / GamesPerPage);

        public IEnumerable<GameListItemViewModel> Games { get; set; }
            = new HashSet<GameListItemViewModel>();
    }
}