using GameReviewHub.Data;
using GameReviewHub.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GameReviewHub.Seed {
    public static class ReviewCommentSeeder {
        public static async Task SeedCommentsAsync(IApplicationBuilder app) {
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
                throw new InvalidOperationException("Seeded users were not found. Cannot seed comments.");
            }

            if (await dbContext.ReviewComments.AnyAsync()) {
                return;
            }

            Review? review1 = await dbContext.Reviews.FirstOrDefaultAsync(r => r.Title == "A modern RPG benchmark");
            Review? review2 = await dbContext.Reviews.FirstOrDefaultAsync(r => r.Title == "Fast, stylish, and addictive");
            Review? review3 = await dbContext.Reviews.FirstOrDefaultAsync(r => r.Title == "One of the best open-world RPGs ever made");

            if (review1 == null || review2 == null || review3 == null) {
                throw new InvalidOperationException("Required seeded reviews were not found. Cannot seed comments.");
            }

            ReviewComment[] comments =
            {
                new ReviewComment
                {
                    ReviewId = review1.Id,
                    UserId = user2.Id,
                    Body = "I completely agree. The companion writing is one of the best parts of the game.",
                    CreatedOn = new DateTime(2023, 8, 11)
                },
                new ReviewComment
                {
                    ReviewId = review2.Id,
                    UserId = user3.Id,
                    Body = "The replayability is amazing. Every run still feels fresh.",
                    CreatedOn = new DateTime(2020, 10, 2)
                },
                new ReviewComment
                {
                    ReviewId = review3.Id,
                    UserId = user1.Id,
                    Body = "The side content and world-building are incredible here.",
                    CreatedOn = new DateTime(2015, 6, 2)
                }
            };

            await dbContext.ReviewComments.AddRangeAsync(comments);
            await dbContext.SaveChangesAsync();
        }
    }
}