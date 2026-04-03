using GameReviewHub.ViewModels.Game;
using GameReviewHub.ViewModels.Game.Admin;

namespace GameReviewHub.Services.Core.Interfaces
{
    public interface IGameService
    {
        Task<IEnumerable<GameListItemViewModel>> ShowAllGamesAsync(string? searchTerm = null, int? genreId = null);
        Task<GameDetailsViewModel?> GetGameDetailsAsync(int gameId);
        Task<CreateGameViewModel> BuildCreateGameViewModelAsync();
        Task<bool> CreateGameAsync(CreateGameInputModel input);
        Task<IEnumerable<GameGenreOptionViewModel>> GetAllGenreOptionsAsync();
        Task<EditGameViewModel?> BuildEditGameViewModelAsync(int gameId);
        Task<bool> ConfirmEditGameAsync(int gameId, CreateGameInputModel input);
        Task<DeleteGameViewModel?> BuildDeleteGameViewModelAsync(int gameId);
        Task<bool> DeleteGameAsync(int gameId);
    }
}
