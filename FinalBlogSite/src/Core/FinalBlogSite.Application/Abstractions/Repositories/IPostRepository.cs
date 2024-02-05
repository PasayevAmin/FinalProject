using FinalBlogSite.Application.Abstractions.Repositories.Generic;
using FinalBlogSite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Application.Abstractions.Repositories
{
    public  interface IPostRepository:IRepository<Post>
    {
    }
}
