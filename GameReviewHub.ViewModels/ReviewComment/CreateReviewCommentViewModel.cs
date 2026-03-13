using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameReviewHub.ViewModels.ReviewComment
{
    public class CreateReviewCommentViewModel
    {
        public int ReviewId { get; set; }
        public int GameId { get; set; }
        public CreateReviewCommentInputModel Input { get; set; } = new();
    }
}
