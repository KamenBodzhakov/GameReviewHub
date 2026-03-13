using Microsoft.AspNetCore.Identity;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameReviewHub.Data.Models
{
    using static Common.ValidationConstants.ReviewComment;
    public class ReviewComment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(BodyMinLength)]
        [MaxLength(BodyMaxLength)]
        public string Body { get; set; } = null!;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        [Required]
        public int ReviewId { get; set; }

        [ForeignKey(nameof(ReviewId))]
        public Review Review { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; } = null!;

    }
}
