using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Application.ViewModels.Comment
{
    public class CommentCreateVM
    {
        public string Content { get; set; }
        public int LikeCount { get; set; }
        public int PostId { get; set; }
    }
}
