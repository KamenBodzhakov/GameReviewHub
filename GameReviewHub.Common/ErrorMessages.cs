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
    }
}