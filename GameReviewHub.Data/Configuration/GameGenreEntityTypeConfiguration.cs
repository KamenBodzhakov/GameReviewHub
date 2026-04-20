using GameReviewHub.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace GameReviewHub.Data.Configuration
{
    using static GameReviewHub.Common.GamesSeedIds;
    using static GameReviewHub.Common.GenresSeedIds;
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
            new GameGenre { GameId = SlayTheSpireGameId, GenreId = DeckbuilderGenreId },

            // Red Dead Redemption 2
            new GameGenre { GameId = 6, GenreId = ActionGenreId },
            new GameGenre { GameId = 6, GenreId = AdventureGenreId },
            new GameGenre { GameId = 6, GenreId = OpenWorldGenreId },


            // The Witcher 3: Wild Hunt
            new GameGenre { GameId = 7, GenreId = RpgGenreId },
            new GameGenre { GameId = 7, GenreId = AdventureGenreId },
            new GameGenre { GameId = 7, GenreId = OpenWorldGenreId },
            
            // Elden Ring
            new GameGenre { GameId = 8, GenreId = RpgGenreId },
            new GameGenre { GameId = 8, GenreId = ActionGenreId },
            new GameGenre { GameId = 8, GenreId = OpenWorldGenreId },
            
            // Against the Storm
            new GameGenre { GameId = 9, GenreId = StrategyGenreId },
            new GameGenre { GameId = 9, GenreId = RoguelikeGenreId },
            new GameGenre { GameId = 9, GenreId = SimulationGenreId },
            
            // Darkest Dungeon II
            new GameGenre { GameId = 10, GenreId = RoguelikeGenreId },
            new GameGenre { GameId = 10, GenreId = StrategyGenreId },
            new GameGenre { GameId = 10, GenreId = TurnBasedGenreId },
            
            // Shape of Dreams
            new GameGenre { GameId = 11, GenreId = ActionGenreId },
            new GameGenre { GameId = 11, GenreId = RoguelikeGenreId },
            
            // Divinity: Original Sin 2
            new GameGenre { GameId = 12, GenreId = RpgGenreId },
            new GameGenre { GameId = 12, GenreId = TurnBasedGenreId },
            new GameGenre { GameId = 12, GenreId = TacticalGenreId },
            new GameGenre { GameId = 12, GenreId = AdventureGenreId },
            
            // Hogwarts Legacy
            new GameGenre { GameId = 13, GenreId = RpgGenreId },
            new GameGenre { GameId = 13, GenreId = AdventureGenreId },
            new GameGenre { GameId = 13, GenreId = OpenWorldGenreId },
            
            // Borderlands 3
            new GameGenre { GameId = 14, GenreId = ShooterGenreId },
            new GameGenre { GameId = 14, GenreId = ActionGenreId },
            new GameGenre { GameId = 14, GenreId = AdventureGenreId },
            
            // Once Human
            new GameGenre { GameId = 15, GenreId = OpenWorldGenreId },
            new GameGenre { GameId = 15, GenreId = ActionGenreId },
            new GameGenre { GameId = 15, GenreId = SandboxGenreId },
        };

        public void Configure(EntityTypeBuilder<GameGenre> entity)
        {
            entity.HasData(this.GameGenres);
        }
    }
}
