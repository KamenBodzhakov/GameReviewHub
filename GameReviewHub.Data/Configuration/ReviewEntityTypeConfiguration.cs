using GameReviewHub.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameReviewHub.Data.Configuration
{
    using static GameReviewHub.Common.GamesSeedIds;

    public class ReviewEntityTypeConfiguration : IEntityTypeConfiguration<Review>
    {

        private readonly Review[] Reviews =
        {
            new Review
            {
                Id = 1,
                GameId = BaldursGate3GameId,
                Title = "A modern RPG benchmark",
                Body =
                    "Pros:\n" +
                    "- Deep role-playing systems and meaningful choices\n" +
                    "- Strong companion writing and voice acting\n" +
                    "- Great tactical combat variety\n\n" +
                    "Cons:\n" +
                    "- Can feel overwhelming early on\n" +
                    "- Occasional pacing dips in long sessions\n\n" +
                    "Overall:\n" +
                    "- An exceptional RPG that rewards exploration and experimentation.",
                Rating = 10,
                CreatedOn = new DateTime(2023, 8, 10)
            },
            new Review
            {
                Id = 2,
                GameId = HadesGameId,
                Title = "Fast, stylish, endlessly replayable",
                Body =
                    "Pros:\n" +
                    "- Tight combat and excellent weapon variety\n" +
                    "- Fantastic music, art direction, and character dialogue\n" +
                    "- Progression feels rewarding run after run\n\n" +
                    "Cons:\n" +
                    "- Repetition can set in if you binge it\n" +
                    "- Some builds depend heavily on luck\n\n" +
                    "Overall:\n" +
                    "- One of the best roguelikes, with story that actually motivates retries.",
                Rating = 9,
                CreatedOn = new DateTime(2020, 10, 1)
            },
            new Review
            {
                Id = 3,
                GameId = WhereWindsMeetGameId, 
                Title = "Huge potential, needs polish",
                Body =
                    "Pros:\n" +
                    "- Beautiful world design and strong wuxia atmosphere\n" +
                    "- Martial arts combat feels unique and expressive\n" +
                    "- Exploration and movement are very satisfying\n\n" +
                    "Cons:\n" +
                    "- Performance can be inconsistent\n" +
                    "- Some systems feel under-explained\n\n" +
                    "Overall:\n" +
                    "- An ambitious action RPG that could be great with refinement and updates.",
                Rating = 8,
                CreatedOn = new DateTime(2025, 12, 1)
            },
            new Review
            {
                Id = 4,
                GameId = OriAndTheBlindForestGameId,
                Title = "Beautiful and emotional platforming",
                Body =
                    "Pros:\n" +
                    "- Stunning visuals and memorable soundtrack\n" +
                    "- Fluid movement and satisfying platforming\n" +
                    "- Strong emotional storytelling\n\n" +
                    "Cons:\n" +
                    "- A few sections have sharp difficulty spikes\n" +
                    "- Combat is simpler than the platforming\n\n" +
                    "Overall:\n" +
                    "- A gorgeous adventure that’s worth experiencing for its world and feel.",
                Rating = 9,
                CreatedOn = new DateTime(2015, 3, 20)
            },
            new Review
            {
                Id = 5,
                GameId = SlayTheSpireGameId, 
                Title = "Strategic deckbuilding perfection",
                Body =
                    "Pros:\n" +
                    "- Deep strategy with tons of build variety\n" +
                    "- Highly replayable with meaningful decisions\n" +
                    "- Excellent balance and difficulty progression\n\n" +
                    "Cons:\n" +
                    "- Visual presentation is minimalistic\n" +
                    "- Runs can swing based on RNG\n\n" +
                    "Overall:\n" +
                    "- A top-tier deckbuilder that stays fresh for hundreds of runs.",
                Rating = 9,
                CreatedOn = new DateTime(2019, 2, 5)
            }
        };

        public void Configure(EntityTypeBuilder<Review> entity)
        {
            //entity.HasData(this.Reviews);
        }
    }
}
