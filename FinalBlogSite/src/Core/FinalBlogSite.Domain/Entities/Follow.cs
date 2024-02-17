using FinalBlogSite.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Domain.Entities
{
    public class Follow:BaseEntity
    {
        public int Id { get; set; }
        public string? FollowerId { get; set; }
        public AppUser? Follower { get; set; }
    }
}
