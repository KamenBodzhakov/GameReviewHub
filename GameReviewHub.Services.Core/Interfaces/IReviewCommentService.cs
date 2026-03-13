
using GameReviewHub.ViewModels.ReviewComment;

namespace GameReviewHub.Services.Core.Interfaces
{
    public interface IReviewCommentService
    {
        Task<bool> AddCommentAsync(int reviewId, CreateReviewCommentInputModel input, string userId);

        Task<List<ReviewCommentViewModel>> GetCommentsByReviewIdAsync(int reviewId);
    }
}
