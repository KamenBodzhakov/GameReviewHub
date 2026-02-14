using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GameReviewHub.Data.Models
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
        public int Rating { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        [Required]
        public int GameId { get; set; }
        public Game Game { get; set; } = null!;

        public string UserId { get; set; } = null!;
        public IdentityUser User { get; set; } = null!;
    }
}
