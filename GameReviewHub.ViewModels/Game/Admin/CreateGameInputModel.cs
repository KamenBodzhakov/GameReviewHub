using System.ComponentModel.DataAnnotations;
using static GameReviewHub.Common.ErrorMessages;
using static GameReviewHub.Common.ValidationConstants.Game;

namespace GameReviewHub.ViewModels.Game.Admin
{
    public class CreateGameInputModel
    {
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

        [Required(ErrorMessage = InvalidReleaseDate)]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        public string? ImagePath { get; set; }

        [MinLength(1, ErrorMessage = GenreRequired)]
        public List<int> SelectedGenreIds { get; set; } = new();
    }
}