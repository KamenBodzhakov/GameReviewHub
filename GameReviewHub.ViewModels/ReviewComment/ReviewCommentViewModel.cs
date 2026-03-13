using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameReviewHub.ViewModels.ReviewComment
{
    public class ReviewCommentViewModel
    {
        public int Id { get; set; }

        public string Body { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public string AuthorUserId { get; set; } = null!;

        public string AuthorUserName { get; set; } = null!;
    }
}
