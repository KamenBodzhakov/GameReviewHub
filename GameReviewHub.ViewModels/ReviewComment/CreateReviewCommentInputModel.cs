using System.ComponentModel.DataAnnotations;
namespace GameReviewHub.ViewModels.ReviewComment
{
    using static Common.ValidationConstants.ReviewComment;
    public class CreateReviewCommentInputModel
    {
        [Required]
        [MinLength(BodyMinLength)]
        [MaxLength(BodyMaxLength)]
        public string Body { get; set; } = null!;
    }
}
