namespace GameReviewHub.Common
{
    public static class EntityValidation
    {
        public static class Game
        {
            public const int GameTitleMaxLength = 100;
            public const int DeveloperMaxLength = 80;
            public const int DescriptionMinLength = 20;
            public const int DescriptionMaxLength = 1000;
        }

        public static class Review
        {
            public const int TitleMinLength = 3;
            public const int TitleMaxLength = 80;
            public const int BodyMinLength = 20;
            public const int BodyMaxLength = 2000;
        }

        public static class Genre
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 30;
        }
    }
}
