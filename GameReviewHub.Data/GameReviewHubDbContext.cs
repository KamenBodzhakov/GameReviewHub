using GameReviewHub.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameReviewHub.Data
{
    public class GameReviewHubDbContext : IdentityDbContext
    {
        public GameReviewHubDbContext(DbContextOptions<GameReviewHubDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Game> Games { get; set; } = null!;

        public virtual DbSet<GameGenre> GamesGenres { get; set; } = null!;

        public virtual DbSet<Genre> Genres { get; set; } = null!;

        public virtual DbSet<Review> Reviews { get; set; } = null!;

        public virtual DbSet<ReviewVote> ReviewVotes { get; set; } = null!;

        public virtual DbSet<ReviewComment> ReviewComments { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GameGenre>().HasKey(gg => new { gg.GameId, gg.GenreId });

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GameReviewHubDbContext).Assembly);

            modelBuilder.Entity<ReviewVote>()
            .HasOne(v => v.Review)
            .WithMany(r => r.Votes)
            .HasForeignKey(v => v.ReviewId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ReviewVote>()
            .HasOne(v => v.User)
            .WithMany()
            .HasForeignKey(v => v.UserId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ReviewComment>()
                .HasOne(c => c.Review)
                .WithMany(r => r.Comments)
                .HasForeignKey(c => c.ReviewId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ReviewComment>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
