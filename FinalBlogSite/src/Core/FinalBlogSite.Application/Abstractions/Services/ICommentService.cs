using FinalBlogSite.Application.ViewModels.Comment;
using FinalBlogSite.Application.ViewModels.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Application.Abstractions.Services
{
    public  interface ICommentService
    {
        Task<IEnumerable<CommentItemVM>> GetAll(int page, int take);

        Task CreateAsync(CommentCreateVM dto);
        Task UpdateAsync(int id, CommentUpdateVM dto);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
        Task ReverseDeleteAsync(int id);
    }
}
