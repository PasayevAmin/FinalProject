using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Application.ViewModels.Post
{
    public class PostUpdateVM
    {
        public string Title { get; set; }
        public string? Images { get; set; }
        public string? Content { get; set; }
        public int? LikeCount { get; set; } = 0;
        public int? CommentCount { get; set; } = 0;
        public int CategoryId { get; set; }

        public virtual ICollection<int>? CommentIds { get; set; }
    }
}
