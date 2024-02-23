using FinalBlogSite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Application.ViewModels
{
    public class CategoryPostVM
    {
        public List<Post> Posts { get; set; }
        public AppUser AppUser { get; set; }
        public List<Category> Categories { get; set; }

    }
}
