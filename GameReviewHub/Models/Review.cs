using System.ComponentModel.DataAnnotations;

namespace GameReviewHub.Models
{
    using static Common.ValidationConstants.Review;
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(TitleMinLength)]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MinLength(BodyMinLength)]
        [MaxLength(BodyMaxLength)] 
        public string Body { get; set; } = null!;

        [Range(1, 10)]
        public int Score { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        [Required]
        public int GameId { get; set; }
        public Game Game { get; set; } = null!;
    }
}
