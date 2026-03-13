
using GameReviewHub.ViewModels.Review;

namespace GameReviewHub.Services.Core.Interfaces
{
    public interface IReviewService
    {
        Task<bool> DeleteReviewAsync(int gameId, int reviewId);

        Task<bool> CreateReviewAsync(int gameId, CreateReviewInputModel input, string userId);

        Task<bool> ConfirmEditReviewAsync(int gameId, int reviewId, CreateReviewInputModel input);

        Task<bool> ReviewExistsAsync(int reviewId);

        Task<CreateReviewViewModel?> BuildCreateReviewViewModelAsync(int gameId);

        Task<DeleteReviewViewModel?> BuildDeleteReviewViewModelAsync(int gameId, int reviewId);

        Task<EditReviewViewModel?> BuildEditReviewViewModelAsync(int gameId, int reviewId);

        Task<List<ReviewListItemViewModel>> GetAllReviewsAsync();

        Task<GameReviewsViewModel?> GetReviewsForGameAsync(int gameId);

    }
}
