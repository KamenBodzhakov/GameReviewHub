using GameReviewHub.ViewModels.Game;

namespace GameReviewHub.Services.Core.Interfaces
{
    public interface IGameService
    {
        Task<IEnumerable<GameListItemViewModel>> ShowAllGamesAsync();
        Task<GameDetailsViewModel?> GetGameDetailsAsync(int gameId);
    }
}
