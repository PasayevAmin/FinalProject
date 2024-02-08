using FinalBlogSite.Application.Abstractions.Services;
using FinalBlogSite.Application.ViewModels.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Persistence.Implementations.Services
{
    public class CommentService : ICommentService
    {
        public Task CreateAsync(CommentCreateVM dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CommentItemVM>> GetAll(int page, int take)
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

        public Task UpdateAsync(int id, CommentUpdateVM dto)
        {
            throw new NotImplementedException();
        }
    }
}
