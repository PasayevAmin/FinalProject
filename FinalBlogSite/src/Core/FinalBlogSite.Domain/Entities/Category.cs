using FinalBlogSite.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Domain.Entities
{
    public class Category:BaseNameableEntity
    {
        public ICollection<Post>? Posts { get; set; }
    }
}
