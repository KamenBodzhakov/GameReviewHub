using GameReviewHub.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameReviewHub.Data
{
    public class GameReviewHubDbContext : IdentityDbContext
    {
        public GameReviewHubDbContext(DbContextOptions<GameReviewHubDbContext> dbContextOptions) 
            : base(dbContextOptions)
        {

        }

        public virtual DbSet<Game> Games { get; set; } = null!;

        public virtual DbSet<GameGenre> GamesGenres { get; set; } = null!;

        public virtual DbSet<Genre> Genres { get; set; } = null!;

        public virtual DbSet<Review> Reviews { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GameGenre>().HasKey(gg => new { gg.GameId, gg.GenreId });

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GameReviewHubDbContext).Assembly);
        }
    }
}
