using FinalBlogSite.Application.ViewModels.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Application.Abstractions.Services
{
    public interface ICategoryService
    {
        Task<ICollection<CategoryItemVM>> GetAllAsync(int page, int take);
        //Task<GetCategoryDto> GetAsync(int id);
        Task Create(CategoryCreateVM dto);
        Task Update(int id, CategoryUpdateVM categoryUpdateDto);
        Task SoftDeleteAsync(int id);
    }
}
