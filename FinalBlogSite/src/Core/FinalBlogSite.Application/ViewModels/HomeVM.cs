using FinalBlogSite.Application.ViewModels;
using FinalBlogSite.Application.ViewModels.Posts;
using FinalBlogSite.Application.ViewModels.Reply;
using FinalBlogSite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Application.ViewModels
{
    public class HomeVM
    {
        public List<Post> LastestPost { get; set; }
        public List<Post> RecendPost { get; set; }
        public List<Post> TitlePost { get; set; }
        public List<Post> CategoryPost { get; set; }
        public List<Post> LikePost { get; set; }
        public List<Post> Posts { get; set; }
        public AppUser AppUser { get; set; }
        public PostCreateVM? CreatePostVM { get; set; }
        public CommentCreateVM CreateCommentVM { get; set; } = new CommentCreateVM();
        public CreateReplyVM CreateReplyVM { get; set; } = new CreateReplyVM();
    }
}
