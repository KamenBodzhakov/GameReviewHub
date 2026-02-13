
using GameReviewHub.Data.Models;
using GameReviewHub.ViewModels;
using GameReviewHub.ViewModels.Review;

namespace GameReviewHub.Services.Core.Interfaces
{
    public interface IReviewService
    {
        Game? GetGameWithReviews(int gameId);

        CreateReviewViewModel? BuildCreateReviewViewModel(int gameId);

        public bool CreateReview(int gameId, CreateReviewInputModel input); // public?

        DeleteReviewViewModel? BuildDeleteReviewViewModel(int gameId, int reviewId);
        bool DeleteReview(int gameId, int reviewId);

        EditReviewViewModel? BuildEditReviewViewModel(int gameId, int reviewId);
        bool ConfirmEditReview(int gameId, int reviewId, CreateReviewInputModel input);
    }
}
