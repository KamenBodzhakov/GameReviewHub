namespace GameReviewHub.Common
{
    public static class ErrorMessages
    {
        public const string UnexpectedError = "An unexpected error occurred. Please try again later.";
        public const string VoteAlreadySubmitted = "You have already voted for this review.";
        public const string VoteUnexpectedError = "An unexpected error occurred while processing your vote.";
        public const string CommentCreationFailed = "Failed to create comment. Please try again.";
        public const string ReviewCreationFailed = "Unable to create review.";
        public const string ReviewEditingFailed = "Unable to edit review.";
        public const string ReviewDeletionFailed = "Unable to delete review.";

        public const string GameCreationFailed = "Unable to create game.";
        public const string InvalidGenreSelection = "Invalid genre selection.";
        public const string GenreRequired = "Select at least one genre.";
        public const string InvalidReleaseDate = "Enter a valid release date.";
        public const string GameEditingFailed = "Unable to edit game.";
    }
}