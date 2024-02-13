using FinalBlogSite.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Application.ViewModels.Posts
{
    public class PostCreateVM
    {
        public string Title { get; set; }
        public IFormFile? Photo { get; set; }
        public string? Content { get; set; }
        public int? LikeCount { get; set; } = 0;
        public int? CommentCount { get; set; } = 0;
        public int CategoryId { get; set; }
        public List<Category>? Categories { get; set; }
        public int AuthorId { get; set; }
        public List<AppUser>? Authors { get; set; }
    }
}
