using GameReviewHub.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameReviewHub.Data.Configuration
{
    using static GameReviewHub.Common.GenresSeedIds;
    public class GenreEntityTypeConfiguration : IEntityTypeConfiguration<Genre>
    {
        private readonly Genre[] Genres =
        {
            new Genre { Id = RpgGenreId, Name = "RPG" },
            new Genre { Id = TurnBasedGenreId, Name = "Turn-Based" },
            new Genre { Id = TacticalGenreId, Name = "Tactical" },
            new Genre { Id = AdventureGenreId, Name = "Adventure" },
            new Genre { Id = ActionGenreId, Name = "Action" },
            new Genre { Id = RoguelikeGenreId, Name = "Roguelike" },
            new Genre { Id = OpenWorldGenreId, Name = "Open World" },
            new Genre { Id = PlatformerGenreId, Name = "Platformer" },
            new Genre { Id = StrategyGenreId, Name = "Strategy" },
            new Genre { Id = DeckbuilderGenreId, Name = "Deckbuilder" },
            new Genre { Id = ShooterGenreId, Name = "Shooter" },
            new Genre { Id = SimulationGenreId, Name = "Simulation" },
            new Genre { Id = RacingGenreId, Name = "Racing" },
            new Genre { Id = SportsGenreId, Name = "Sports" },
            new Genre { Id = PuzzleGenreId, Name = "Puzzle" },
            new Genre { Id = HorrorGenreId, Name = "Horror" },
            new Genre { Id = SandboxGenreId, Name = "Sandbox" },
            new Genre { Id = MmoGenreId, Name = "MMO" }
        };

        public void Configure(EntityTypeBuilder<Genre> entity)
        {
            entity.HasData(this.Genres);
        }
    }
}
