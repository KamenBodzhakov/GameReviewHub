using GameReviewHub.Models.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace GameReviewHub.Data.Configuration
{
    using static GameReviewHub.Data.Common.GamesSeedIds;
    using static Common.GenresSeedIds;
    public class GameGenreEntityTypeConfiguration : IEntityTypeConfiguration<GameGenre>
    {
        private readonly GameGenre[] GameGenres =
        {
            // Baldur's Gate 3
            new GameGenre { GameId = BaldursGate3GameId, GenreId = RpgGenreId },
            new GameGenre { GameId = BaldursGate3GameId, GenreId = TurnBasedGenreId },
            new GameGenre { GameId = BaldursGate3GameId, GenreId = TacticalGenreId },
            new GameGenre { GameId = BaldursGate3GameId, GenreId = AdventureGenreId },

            // Hades
            new GameGenre { GameId = HadesGameId, GenreId = ActionGenreId },
            new GameGenre { GameId = HadesGameId, GenreId = RoguelikeGenreId },

            // Where Winds Meet
            new GameGenre { GameId = WhereWindsMeetGameId, GenreId = ActionGenreId },
            new GameGenre { GameId = WhereWindsMeetGameId, GenreId = RpgGenreId },
            new GameGenre { GameId = WhereWindsMeetGameId, GenreId = AdventureGenreId },
            new GameGenre { GameId = WhereWindsMeetGameId, GenreId = OpenWorldGenreId },

            // Ori and the Blind Forest
            new GameGenre { GameId = OriAndTheBlindForestGameId, GenreId = PlatformerGenreId },
            new GameGenre { GameId = OriAndTheBlindForestGameId, GenreId = AdventureGenreId },

            // Slay the Spire
            new GameGenre { GameId = SlayTheSpireGameId, GenreId = RoguelikeGenreId },
            new GameGenre { GameId = SlayTheSpireGameId, GenreId = StrategyGenreId },
            new GameGenre { GameId = SlayTheSpireGameId, GenreId = DeckbuilderGenreId }
        };

        public void Configure(EntityTypeBuilder<GameGenre> entity)
        {
            entity.HasData(this.GameGenres);
        }
    }
}
