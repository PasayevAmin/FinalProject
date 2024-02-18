using FinalBlogSite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Application.ViewModels.Follows
{
    public class CreateFollowVM
    {
        public string? FollowerId { get; set; }
        public List<AppUser>? Follower { get; set; }
    }
}
