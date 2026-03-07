
namespace GameReviewHub.Services.Core.Interfaces
{
    public interface IReviewVoteService
    {
        Task<bool> VoteAsync(int reviewId, string userId, bool isUpvote);

        Task<int> GetVoteCountAsync(int reviewId);

        Task<bool> HasUserVotedAsync(int reviewId, string userId);

    }
}
