using GameReviewHub.Data;
using Microsoft.EntityFrameworkCore;

namespace GameReviewHub.Services.Tests.Helpers
{
    public static class DbContextMockHelper
    {
        public static GameReviewHubDbContext CreateInMemoryDbContext(string databaseName)
        {
            DbContextOptions<GameReviewHubDbContext> options =
                new DbContextOptionsBuilder<GameReviewHubDbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;

            return new GameReviewHubDbContext(options);
        }
    }
}