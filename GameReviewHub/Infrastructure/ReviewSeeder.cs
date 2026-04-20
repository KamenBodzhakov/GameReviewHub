using GameReviewHub.Data;
using GameReviewHub.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GameReviewHub.Seed {
    public static class ReviewSeeder {
        public static async Task SeedReviewsAsync(IApplicationBuilder app) {
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
                throw new InvalidOperationException("Seeded users were not found. Reviews cannot be seeded.");
            }

            if (await dbContext.Reviews.AnyAsync()) {
                return;
            }

            Review[] reviews =
            {
                new Review
                {
                    Title = "A modern RPG benchmark",
                    Body = "Deep role-playing systems, strong companions, and excellent tactical combat. One of the strongest RPGs in recent years.",
                    Rating = 10,
                    CreatedOn = new DateTime(2023, 8, 10),
                    GameId = 1,
                    UserId = user1.Id
                },
                new Review
                {
                    Title = "Fast, stylish, and addictive",
                    Body = "Great combat feel, excellent voice acting, and a loop that stays engaging for a long time.",
                    Rating = 9,
                    CreatedOn = new DateTime(2020, 10, 1),
                    GameId = 2,
                    UserId = user2.Id
                },
                new Review
                {
                    Title = "Beautiful but still rough in places",
                    Body = "Strong atmosphere and satisfying exploration, though some systems still need polish.",
                    Rating = 8,
                    CreatedOn = new DateTime(2025, 12, 1),
                    GameId = 3,
                    UserId = user3.Id
                },
                new Review
                {
                    Title = "Emotional and fluid platforming",
                    Body = "A gorgeous game with memorable music and a touching presentation.",
                    Rating = 9,
                    CreatedOn = new DateTime(2015, 3, 20),
                    GameId = 4,
                    UserId = user1.Id
                },
                new Review
                {
                    Title = "Replayability done right",
                    Body = "Excellent strategic depth and a rewarding roguelike structure that stays fun for many runs.",
                    Rating = 9,
                    CreatedOn = new DateTime(2019, 2, 5),
                    GameId = 5,
                    UserId = user2.Id
                },
                new Review
                {
                    Title = "One of the best open-world RPGs ever made",
                    Body = "Fantastic quests, strong writing, and meaningful side content make this a standout experience.",
                    Rating = 10,
                    CreatedOn = new DateTime(2015, 6, 1),
                    GameId = 7,
                    UserId = user3.Id
                },
                new Review
                {
                    Title = "Challenging, rewarding, and atmospheric",
                    Body = "A huge world full of discovery, difficult combat, and a strong sense of mystery.",
                    Rating = 10,
                    CreatedOn = new DateTime(2022, 3, 10),
                    GameId = 8,
                    UserId = user1.Id
                },
                new Review
                {
                    Title = "An immersive western masterpiece",
                    Body = "Incredible storytelling, detailed world, and unforgettable characters make this one of Rockstar's best.",
                    Rating = 10,
                    CreatedOn = new DateTime(2018, 11, 1),
                    GameId = 6,
                    UserId = user2.Id
                },

            };

            await dbContext.Reviews.AddRangeAsync(reviews);
            await dbContext.SaveChangesAsync();
        }
    }
}