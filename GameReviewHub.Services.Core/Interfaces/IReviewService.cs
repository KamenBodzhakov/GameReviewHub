
using GameReviewHub.Data.Models;
using GameReviewHub.ViewModels;
using GameReviewHub.ViewModels.Review;

namespace GameReviewHub.Services.Core.Interfaces
{
    public interface IReviewService
    {
        Task<CreateReviewViewModel?> BuildCreateReviewViewModelAsync(int gameId);

        Task<bool> CreateReviewAsync(int gameId, CreateReviewInputModel input, string userId);

        Task<DeleteReviewViewModel?> BuildDeleteReviewViewModelAsync(int gameId, int reviewId);
        Task<bool> DeleteReviewAsync(int gameId, int reviewId);

        Task<EditReviewViewModel?> BuildEditReviewViewModelAsync(int gameId, int reviewId);
        Task<bool> ConfirmEditReviewAsync(int gameId, int reviewId, CreateReviewInputModel input);

        Task<List<ReviewListItemViewModel>> GetAllReviewsAsync();
        Task<GameReviewsViewModel?> GetReviewsForGameAsync(int gameId);

    }
}
