using GameReviewHub.Data;
using GameReviewHub.Data.Models;
using GameReviewHub.Services.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameReviewHub.Services.Core
{
    public class ReviewVoteService : IReviewVoteService
    {
        private readonly GameReviewHubDbContext dbContext;

        public ReviewVoteService(GameReviewHubDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<int> GetVoteCountAsync(int reviewId)
        {
            return await dbContext.ReviewVotes
                .Where(rv => rv.ReviewId == reviewId && rv.IsUpvote)
                .CountAsync();

        }

        public async Task<bool> HasUserVotedAsync(int reviewId, string userId)
        {
            return await dbContext.ReviewVotes
                .AnyAsync(rv => rv.ReviewId == reviewId && rv.UserId == userId);
        }

        public async Task<bool> VoteAsync(int reviewId, string userId, bool isUpvote)
        {
            bool reviewExists = await dbContext.Reviews
                .AnyAsync(r => r.Id == reviewId);

            if (!reviewExists)
                return false;

            ReviewVote? voteExists = await dbContext.ReviewVotes
                .FirstOrDefaultAsync(rv => rv.ReviewId == reviewId && rv.UserId == userId);

            if (voteExists == null)
            {
                ReviewVote vote = new ReviewVote
                {
                    ReviewId = reviewId,
                    UserId = userId,
                    IsUpvote = isUpvote
                };

                await dbContext.ReviewVotes.AddAsync(vote);
            }
            else
            {
                if (voteExists.IsUpvote == isUpvote)
                {
                    return false;
                }

                voteExists.IsUpvote = isUpvote;
            }

            await dbContext.SaveChangesAsync();

            return true;
        }

    }
}
