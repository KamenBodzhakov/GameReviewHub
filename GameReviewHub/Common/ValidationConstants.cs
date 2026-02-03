namespace GameReviewHub.Common
{
    public static class ValidationConstants
    {
        public static class Game
        {
            public const int GameTitleMaxLength = 100;
            public const int DeveloperMaxLength = 80;
            public const int DescriptionMinLength = 20;
            public const int DescriptionMaxLength = 1000;
            public const int GameCardMaxDescriptionLength = 200;
        }

        public static class Review
        {
            public const int TitleMinLength = 2;
            public const int TitleMaxLength = 80;
            public const int BodyMinLength = 5;
            public const int BodyMaxLength = 2000;
            public const int ScoreMinValue = 1;
            public const int ScoreMaxValue = 10;
        }

        public static class Genre
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 30;
        }
        
    }
}
