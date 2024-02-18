using AutoMapper;
using FinalBlogSite.Application.Abstractions.Repositories;
using FinalBlogSite.Application.Abstractions.Services;
using FinalBlogSite.Application.ViewModels;
using FinalBlogSite.Application.ViewModels.Categorys;
using FinalBlogSite.Domain.Entities;
using FinalBlogSite.MVC.MiddleWears.Exseptions;
using FinalBlogSite.Persistence.DAL;
using FinalBlogSite.Persistence.Implementations.Repositories;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Persistence.Implementations.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public CategoryService(ICategoryRepository repository, IMapper mapper,AppDbContext context)
        {
            _repository = repository;
            _mapper = mapper;
            _context = context;
        }
       
        public async Task<bool> Create(CategoryCreateVM Vm,ModelStateDictionary modelstate)
        {
            if (!modelstate.IsValid)
            {
                return false;
            }
            if (await _repository.IsExist(l => l.Name == Vm.Name))
            {
                modelstate.AddModelError("Name", "This Name is already exist");
                return false;
            }
            await _repository.AddAsync(_mapper.Map<Category>(Vm));
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<CategoryUpdateVM> Update(int id)
        {
            if (id <= 0) throw new WrongRequestExceptions("Bad Request");

            Category category = await _repository.GetByIdAsync(id, true);

            if (category == null) throw new NotFoundExceptions("Not Found");
            CategoryUpdateVM categoryVM = new CategoryUpdateVM
            {
                Name = category.Name,
            };
            return categoryVM;

        }



        public async Task<bool> Delete(int id)
        {
            if (id <= 0) throw new WrongRequestExceptions("Bad Request");

            Category existed = await _repository.GetByIdAsync(id, true);

            if (existed == null) throw new NotFoundExceptions("Not Found");

            _repository.Delete(existed);

            await _repository.SaveChangesAsync();
            return true;

           
        }

        public async Task<bool> UpdatePost(int id, CategoryUpdateVM vM,ModelStateDictionary modelstate)
        {
            if (id <= 0) throw new WrongRequestExceptions("Bad Request");

            Category existed = await _repository.GetByIdAsync(id, true);

            if (existed == null) throw new NotFoundExceptions("Not Found");

            //if (await _repository.IsExist(l => l.Name == vM.Name&& l.Id==id))
            //{
            //    modelstate.AddModelError("Name", "This Category is already exist");
            //    return false;
            //}

            existed.Name = vM.Name;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<PaginationVM<Category>> GetAllAsync(int page = 1, int take = 3)
        {
            if (page < 1 || take < 1) throw new WrongRequestExceptions("Bad request");
            ICollection<Category> categories = await _repository.GetAllWhere(skip: (page - 1) * take, take: take, orderexpression: x => x.Id, isDescending: true).ToListAsync();
            if (categories == null) throw new NotFoundExceptions("Not Found");
            int count = await _repository.GetAll().CountAsync();
            if (count < 0) throw new NotFoundExceptions("Not Found");
            double totalpage = Math.Ceiling((double)count / take);
            PaginationVM<Category> vm = new PaginationVM<Category>
            {
                Items = categories.ToList(),
                CurrentPage = page,
                TotalPage = totalpage
            };
            return vm;
        }

    }
}
