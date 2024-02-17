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
        public List<Post> AuthorPost { get; set; }
        public List<AppUser> AppUser { get; set; }
        public PaginationVM<Post> Posts { get; set; }

    }
}
