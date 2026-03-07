using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameReviewHub.Data.Models
{
    [Index(nameof(ReviewId), nameof(UserId), IsUnique = true)]
    public class ReviewVote
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ReviewId { get; set; }

        [ForeignKey(nameof(ReviewId))]
        public Review Review { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; } = null!;

        public bool IsUpvote { get; set; }
    }
}
