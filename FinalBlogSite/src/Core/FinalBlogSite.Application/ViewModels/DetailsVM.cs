using FinalBlogSite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Application.ViewModels
{
    public class DetailsVM
    {
        public Post Post { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
