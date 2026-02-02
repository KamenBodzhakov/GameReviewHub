using System.ComponentModel.DataAnnotations;

namespace GameReviewHub.Models
{
    using static Common.ValidationConstants.Genre;
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<GameGenre> GameGenres { get; set; } = new HashSet<GameGenre>();

    }
}
