using FinalBlogSite.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Domain.Entities
{
    public class Comment:BaseEntity
    {
        public string Content { get; set; }
        public int LikeCount { get; set; }
        public int? PostId { get; set; }
        public Post? Post { get; set; }
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
