using FinalBlogSite.Application.ViewModels.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Application.ViewModels.Post
{
    public class PostGetVM
    {
        public string Title { get; set; }
        public string? Images { get; set; }
        public string? Content { get; set; }
        public int? LikeCount { get; set; } = 0;
        public int? CommentCount { get; set; } = 0;
        public int CategoryId { get; set; }
        public IncludeCategoryVM Category { get; set; }
    }
}
