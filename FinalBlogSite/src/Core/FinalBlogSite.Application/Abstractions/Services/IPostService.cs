

using FinalBlogSite.Application.ViewModels;
using FinalBlogSite.Application.ViewModels.Posts;
using FinalBlogSite.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FinalBlogSite.Application.Abstractions.Services
{
    public  interface IPostService
    {

        Task<bool> CreateAsync(PostCreateVM vm, ModelStateDictionary modelstate);
        Task<PostCreateVM> CreatedAsync(PostCreateVM vm);
        Task<bool> DeleteAsync(int id);
        Task<PaginationVM<Post>> GetAllAsync(int page = 1, int take = 3);
        Task<bool> UpdateAsync(int id, PostUpdateVM vm, ModelStateDictionary modelstate);
        Task<PostUpdateVM> UpdatedAsync(int id, PostUpdateVM vm);
        Task<bool> Liked(int id);

    }
}
