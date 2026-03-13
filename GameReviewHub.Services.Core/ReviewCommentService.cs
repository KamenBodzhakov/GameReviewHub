using GameReviewHub.Data;
using GameReviewHub.Data.Models;
using GameReviewHub.Services.Core.Interfaces;
using GameReviewHub.ViewModels.ReviewComment;
using Microsoft.EntityFrameworkCore;

namespace GameReviewHub.Services.Core
{
    public class ReviewCommentService : IReviewCommentService
    {
        private readonly GameReviewHubDbContext dbContext;

        public ReviewCommentService(GameReviewHubDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> AddCommentAsync(int reviewId, CreateReviewCommentInputModel input, string userId)
        {
            bool reviewExists = dbContext.Reviews.Any(r => r.Id == reviewId);
            if (!reviewExists)
            {
                return false;
            }

                ReviewComment reviewComment = new ReviewComment
                {
                    Body = input.Body,
                    ReviewId = reviewId,
                    UserId = userId,
                    CreatedOn = DateTime.UtcNow
                };

            await dbContext.ReviewComments.AddAsync(reviewComment);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<ReviewCommentViewModel>> GetCommentsByReviewIdAsync(int reviewId)
        {
            return await dbContext.ReviewComments
                .Where(rc => rc.ReviewId == reviewId)
                .OrderBy(rc => rc.CreatedOn)
                .Select(rc => new ReviewCommentViewModel
                {
                    Id = rc.Id,
                    Body = rc.Body,
                    CreatedOn = rc.CreatedOn,
                    AuthorUserId = rc.UserId,
                    AuthorUserName = rc.User.UserName!
                })
                .ToListAsync();
        }

    }
}
