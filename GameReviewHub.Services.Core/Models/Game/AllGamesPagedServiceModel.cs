using GameReviewHub.ViewModels.Game;

namespace GameReviewHub.Services.Core
{
    public class AllGamesPagedServiceModel
    {
        public int TotalGamesCount { get; set; }

        public int GamesPerPage = 6;

        public IEnumerable<GameListItemViewModel> Games { get; set; }
            = new HashSet<GameListItemViewModel>();
    }
}