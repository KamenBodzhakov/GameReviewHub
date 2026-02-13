using GameReviewHub.ViewModels.Game;

namespace GameReviewHub.Services.Core.Interfaces
{
    public interface IGameService
    {
        IEnumerable<GameListItemViewModel> ShowAllGames();
        GameDetailsViewModel? GetGameDetails(int gameId);
    }
}
