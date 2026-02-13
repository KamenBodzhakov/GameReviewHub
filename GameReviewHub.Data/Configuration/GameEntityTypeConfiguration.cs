using GameReviewHub.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameReviewHub.Data.Configuration
{
    public class GameEntityTypeConfiguration : IEntityTypeConfiguration<Game>
    {
        private readonly Game[] Games =
        {
            new Game
            {
                Id = 1,
                Title = "Baldur's Gate 3",
                Developer = "Larian Studios",
                Description = "A sprawling role-playing adventure in the Dungeons & Dragons universe, featuring deep narrative, tactical combat, and rich character progression.",
                ReleaseDate = new DateTime(2023, 8, 3),
                ImagePath = "/images/games/baldurs-gate-3.jpg"
            },
            new Game
            {
                Id = 2,
                Title = "Hades",
                Developer = "Supergiant Games",
                Description = "A fast-paced roguelike action game where players fight to escape the Underworld, combining tight combat with evolving storytelling.",
                ReleaseDate = new DateTime(2020, 9, 17),
                ImagePath = "/images/games/hades.jpg"
            },
            new Game
            {
                Id = 3,
                Title = "Where Winds Meet",
                Developer = "Everstone Studio",
                Description = "A wuxia-inspired open-world action-adventure RPG set in a fantastical version of historical China, blending martial arts combat with exploration.",
                ReleaseDate = new DateTime(2025, 11, 14),
                ImagePath = "/images/games/where-winds-meet.jpg"
            },
            new Game
            {
                Id = 4,
                Title = "Ori and the Blind Forest",
                Developer = "Moon Studios",
                Description = "A visually stunning platform adventure about a young guardian spirit on a touching journey to save a dying forest filled with heart and challenge.",
                ReleaseDate = new DateTime(2015, 3, 11),
                ImagePath = "/images/games/ori-and-the-blind-forest.jpg"
            },
            new Game
            {
                Id = 5,
                Title = "Slay the Spire",
                Developer = "Mega Crit",
                Description = "A beloved roguelike deckbuilder that blends strategic card combat with procedural levels, challenging players to ascend the mysterious Spire.",
                ReleaseDate = new DateTime(2019, 1, 23),
                ImagePath = "/images/games/slay-the-spire.jpg"
            },
            new Game
            {
                 Id = 6,
                 Title = "Red Dead Redemption 2",
                 Developer = "Rockstar Games",
                 Description = "An epic story set at the dawn of modern America. Follow Arthur Morgan and the Van der Linde gang as they struggle to survive in a changing world.",
                 ReleaseDate = new DateTime(2018, 10, 26),
                 ImagePath = "/images/games/red-dead-redemption-2.jpg"
            }
                };

        public void Configure(EntityTypeBuilder<Game> entity)
        {
            entity.HasData(this.Games);
        }
    }
}
