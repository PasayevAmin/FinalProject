using FinalBlogSite.Application.Abstractions.Services;
using FinalBlogSite.Application.ViewModels.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Persistence.Implementations.Services
{
    public class CategoryService : ICategoryService
    {
        public Task Create(CategoryCreateVM dto)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<CategoryItemVM>> GetAllAsync(int page, int take)
        {
            throw new NotImplementedException();
        }

        public Task SoftDeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(int id, CategoryUpdateVM categoryUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
