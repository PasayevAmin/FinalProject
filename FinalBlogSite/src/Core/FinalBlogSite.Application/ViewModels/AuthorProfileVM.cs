using FinalBlogSite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Application.ViewModels
{
    public class AuthorProfileVM
    {
        public List<Category> Categories { get; set; }
        public List<Post> AuthorPost { get; set; }
        public List<AppUser> AppUsers { get; set; }
        public AppUser AppUser { get; set; }

        public PaginationVM<Post> Posts { get; set; }
        public List<Follow> Follows { get; set; }

    }
}
