using FinalBlogSite.Application.ViewModels.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Application.Abstractions.Services
{
    public  interface IPostService
    {
        Task<IEnumerable<PostItemVM>> GetAll(int page, int take);
        Task<PostGetVM> GetByIdAsync(int id);

        Task CreateAsync(PostCreateVM dto);
        Task UpdateAsync(int id, PostUpdateVM dto);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
        Task ReverseDeleteAsync(int id);
    }
}
