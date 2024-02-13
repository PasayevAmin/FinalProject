using FinalBlogSite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Application.ViewModels
{
    public class ProfileVM
    {
        public List<Post> LastestPost { get; set; }
        public List<Post> RecendPost { get; set; }
        public List<AppUser> AppUser { get; set; }
        public List<Post> CategoryPost { get; set; }

    }
}
