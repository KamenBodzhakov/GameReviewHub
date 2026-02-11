
using GameReviewHub.Data.Models;
using GameReviewHub.ViewModels.Review;

namespace GameReviewHub.Services.Core.Interfaces
{
    public interface IReviewService
    {
        Game? GetGameWithReviews(int gameId);

        CreateReviewViewModel? BuildCreateReviewViewModel(int gameId);
    }
}
