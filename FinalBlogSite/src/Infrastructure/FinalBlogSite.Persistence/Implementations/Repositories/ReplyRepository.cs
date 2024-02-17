using FinalBlogSite.Application.Abstractions.Repositories;
using FinalBlogSite.Domain.Entities;
using FinalBlogSite.Persistence.DAL;
using FinalBlogSite.Persistence.Implementations.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Persistence.Implementations.Repositories
{
    public class ReplyRepository:Repository<Reply>,IReplyRepository
    {
        public ReplyRepository(AppDbContext context):base(context)
        {
            
        }
    }
}
