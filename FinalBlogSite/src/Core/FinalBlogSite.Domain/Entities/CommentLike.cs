using FinalBlogSite.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Domain.Entities
{
    public class CommentLike:BaseEntity
    {
        public string? LikerId { get; set; }
        public int CommentId { get; set; }
        public AppUser? Liker { get; set; }
        public Comment? Comment { get; set; }
    }
}
