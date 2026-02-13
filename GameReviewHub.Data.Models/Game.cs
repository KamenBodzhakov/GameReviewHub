using System.ComponentModel.DataAnnotations;

namespace GameReviewHub.Data.Models
{
    using static Common.ValidationConstants.Game;
    public class Game
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(GameTitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(DeveloperMaxLength)]
        public string Developer { get; set; } = null!;

        [Required]
        [MinLength(DescriptionMinLength)]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [DataType(DataType.Date)]
        public DateTime ReleaseDate {  get; set; }  

        public string? ImagePath { get; set; }

        public virtual ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
        public virtual ICollection<GameGenre> GameGenres { get; set; } = new HashSet<GameGenre>();
    }
}
