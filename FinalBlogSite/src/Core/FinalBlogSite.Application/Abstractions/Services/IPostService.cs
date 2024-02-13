using FinalBlogSite.Application.ViewModels.Comment;
using FinalBlogSite.Application.ViewModels;
using FinalBlogSite.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalBlogSite.Application.ViewModels.Posts;

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

    }
}
