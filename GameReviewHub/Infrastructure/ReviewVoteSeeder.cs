using GameReviewHub.Data;
using GameReviewHub.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GameReviewHub.Seed {
    public static class ReviewVoteSeeder {
        public static async Task SeedVotesAsync(IApplicationBuilder app) {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            IServiceProvider services = scope.ServiceProvider;

            GameReviewHubDbContext dbContext =
                services.GetRequiredService<GameReviewHubDbContext>();

            UserManager<IdentityUser> userManager =
                services.GetRequiredService<UserManager<IdentityUser>>();

            IdentityUser? user1 = await userManager.FindByEmailAsync("user1@gamereviewhub.com");
            IdentityUser? user2 = await userManager.FindByEmailAsync("user2@gamereviewhub.com");
            IdentityUser? user3 = await userManager.FindByEmailAsync("user3@gamereviewhub.com");

            if (user1 == null || user2 == null || user3 == null) {
                throw new InvalidOperationException("Seeded users were not found. Cannot seed votes.");
            }

            if (await dbContext.ReviewVotes.AnyAsync()) {
                return;
            }

            Review? review1 = await dbContext.Reviews.FirstOrDefaultAsync(r => r.Title == "A modern RPG benchmark");
            Review? review2 = await dbContext.Reviews.FirstOrDefaultAsync(r => r.Title == "Fast, stylish, and addictive");
            Review? review3 = await dbContext.Reviews.FirstOrDefaultAsync(r => r.Title == "One of the best open-world RPGs ever made");
            Review? review4 = await dbContext.Reviews.FirstOrDefaultAsync(r => r.Title == "Challenging, rewarding, and atmospheric");

            if (review1 == null || review2 == null || review3 == null || review4 == null) {
                throw new InvalidOperationException("Required seeded reviews were not found. Cannot seed votes.");
            }

            ReviewVote[] votes =
            {
                new ReviewVote
                {
                    ReviewId = review1.Id,
                    UserId = user2.Id,
                    IsUpvote = true
                },
                new ReviewVote
                {
                    ReviewId = review1.Id,
                    UserId = user3.Id,
                    IsUpvote = true
                },
                new ReviewVote
                {
                    ReviewId = review2.Id,
                    UserId = user1.Id,
                    IsUpvote = true
                },
                new ReviewVote
                {
                    ReviewId = review3.Id,
                    UserId = user2.Id,
                    IsUpvote = true
                },
                new ReviewVote
                {
                    ReviewId = review4.Id,
                    UserId = user1.Id,
                    IsUpvote = true
                }
            };

            await dbContext.ReviewVotes.AddRangeAsync(votes);
            await dbContext.SaveChangesAsync();
        }
    }
}