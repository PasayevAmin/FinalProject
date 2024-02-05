using FinalBlogSite.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Application.Abstractions.Repositories.Generic
{
    public interface IRepository<T> where T : BaseEntity, new()
    {
    }
}
