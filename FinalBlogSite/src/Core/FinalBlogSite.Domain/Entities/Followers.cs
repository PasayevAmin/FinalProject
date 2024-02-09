using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Domain.Entities
{
    public class Followers
    {
        public int Id { get; set; }
        public int FollowerId { get; set; }
        public AppUser Follower { get; set; }
    }
}
