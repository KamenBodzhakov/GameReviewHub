using GameReviewHub.Models.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameReviewHub.Data.Configuration
{
    using static Common.GenresSeedIds;
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
            new Genre { Id = DeckbuilderGenreId, Name = "Deckbuilder" }
        };

        public void Configure(EntityTypeBuilder<Genre> entity)
        {
            entity.HasData(this.Genres);
        }
    }
}
