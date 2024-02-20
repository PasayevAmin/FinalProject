using FinalBlogSite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Application.ViewModels
{
    public class CommentUpdateVM
    {
        public string Content { get; set; }
        public int LikeCount { get; set; }
        public int? PostId { get; set; }
        public DateTime CreateAt { get; set; }
        public List<Post>? Posts { get; set; }


    }
}
