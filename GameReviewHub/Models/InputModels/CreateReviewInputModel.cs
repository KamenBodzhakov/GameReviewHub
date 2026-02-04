namespace GameReviewHub.Models.InputModels
{
    using System.ComponentModel.DataAnnotations;
    using static Common.ValidationConstants.Review;
    public class CreateReviewInputModel
    {
        [Required]
        [MinLength(TitleMinLength)]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MinLength(BodyMinLength)]
        [MaxLength(BodyMaxLength)]
        public string Body { get; set; } = null!;

        [Required]
        [Range(ScoreMinValue, ScoreMaxValue)]
        public int Rating { get; set; }

    }
}
