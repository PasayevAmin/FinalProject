using FinalBlogSite.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Domain.Entities
{
    public class Post:BaseEntity
    {
        public string Title { get; set; }
        public string? Images { get; set; }
        public string? Content { get; set; }
        public int? LikeCount { get; set; } = 0;
        public int? CommentCount { get; set; } = 0;
        public string? AuthorId { get; set; }
        public AppUser? Author { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; } = null!;
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<Like>? Likes { get; set; }


    }
}
