using FinalBlogSite.Application.Abstractions.Services;
using FinalBlogSite.Application.ViewModels.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Persistence.Implementations.Services
{
    public class PostService : IPostService
    {
        public Task CreateAsync(PostCreateVM dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PostItemVM>> GetAll(int page, int take)
        {
            throw new NotImplementedException();
        }

        public Task<PostGetVM> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task ReverseDeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task SoftDeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id, PostUpdateVM dto)
        {
            throw new NotImplementedException();
        }
    }
}
