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
            },
            new Game
            {
                Id = 7,
                Title = "The Witcher 3: Wild Hunt",
                Developer = "CD Projekt Red",
                Description = "An expansive open-world RPG following Geralt of Rivia, featuring deep storytelling, memorable characters, and meaningful choices.",
                ReleaseDate = new DateTime(2015, 5, 19),
                ImagePath = "/images/games/witcher-3.jpg"
            },
            new Game
            {
                Id = 8,
                Title = "Elden Ring",
                Developer = "FromSoftware",
                Description = "A challenging open-world action RPG blending Souls-like combat with exploration in a vast fantasy world.",
                ReleaseDate = new DateTime(2022, 2, 25),
                ImagePath = "/images/games/elden-ring.jpg"
            },
            new Game
            {
                Id = 9,
                Title = "Against the Storm",
                Developer = "Eremite Games",
                Description = "A dark fantasy city builder with roguelike elements where players rebuild civilization under constant threat of destruction.",
                ReleaseDate = new DateTime(2023, 12, 8),
                ImagePath = "/images/games/against-the-storm.jpg"
            },
            new Game
            {
                Id = 10,
                Title = "Darkest Dungeon II",
                Developer = "Red Hook Studios",
                Description = "A turn-based roguelike road trip through a decaying world, combining strategic combat with psychological stress mechanics.",
                ReleaseDate = new DateTime(2023, 5, 8),
                ImagePath = "/images/games/darkest-dungeon-2.jpg"
            },
            new Game
            {
                Id = 11,
                Title = "Shape of Dreams",
                Developer = "NEOWIZ",
                Description = "An action roguelite with fast-paced combat and dreamlike visuals, focused on skill-based gameplay and replayability.",
                ReleaseDate = new DateTime(2024, 5, 10),
                ImagePath = "/images/games/shape-of-dreams.jpg"
            },
            new Game
            {
                Id = 12,
                Title = "Divinity: Original Sin 2",
                Developer = "Larian Studios",
                Description = "A deep tactical RPG featuring cooperative gameplay, rich storytelling, and highly interactive environments.",
                ReleaseDate = new DateTime(2017, 9, 14),
                ImagePath = "/images/games/divinity-original-sin-2.jpg"
            },
            new Game
            {
                Id = 13,
                Title = "Hogwarts Legacy",
                Developer = "Avalanche Software",
                Description = "An open-world action RPG set in the Wizarding World, allowing players to explore Hogwarts and master magic.",
                ReleaseDate = new DateTime(2023, 2, 10),
                ImagePath = "/images/games/hogwarts-legacy.jpg"
            },
            new Game
            {
                Id = 14,
                Title = "Borderlands 3",
                Developer = "Gearbox Software",
                Description = "A fast-paced looter shooter with cooperative gameplay, outrageous weapons, and a stylized sci-fi world.",
                ReleaseDate = new DateTime(2019, 9, 13),
                ImagePath = "/images/games/borderlands-3.jpg"
            },
            new Game
            {
                Id = 15,
                Title = "Once Human",
                Developer = "Starry Studio",
                Description = "A multiplayer open-world survival game set in a post-apocalyptic world with supernatural elements and base-building mechanics.",
                ReleaseDate = new DateTime(2024, 7, 9),
                ImagePath = "/images/games/once-human.jpg"
            }
                };

        public void Configure(EntityTypeBuilder<Game> entity)
        {
            entity.HasData(this.Games);
        }
    }
}
