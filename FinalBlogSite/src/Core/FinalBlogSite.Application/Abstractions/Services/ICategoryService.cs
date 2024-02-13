using FinalBlogSite.Application.ViewModels;
using FinalBlogSite.Application.ViewModels.Categorys;
using FinalBlogSite.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Application.Abstractions.Services
{
    public interface ICategoryService
    {
        
        Task<bool> Create(CategoryCreateVM vm,ModelStateDictionary keyValuePairs);
        Task<CategoryUpdateVM> Update(int id);
        Task<bool> UpdatePost(int id, CategoryUpdateVM vM,ModelStateDictionary keyValuePairs);
        Task<bool> Delete(int id);
        Task<PaginationVM<Category>> GetAllAsync(int page = 1, int take = 3);
        }
}
