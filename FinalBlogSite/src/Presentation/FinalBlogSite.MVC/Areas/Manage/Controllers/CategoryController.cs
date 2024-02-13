using FinalBlogSite.Application.Abstractions.Services;
using FinalBlogSite.Application.ViewModels;
using FinalBlogSite.Application.ViewModels.Categorys;
using FinalBlogSite.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FinalBlogSite.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index(int page = 1, int take = 3)
        {
            PaginationVM<Category> vm = await _categoryService.GetAllAsync(page, take);
            if (vm.Items == null) return NotFound();
            return View(vm);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateVM vM)
        {
           bool result= await _categoryService.Create(vM,ModelState);
            if (result) return RedirectToAction("index");
            return View(vM);
        }
        public async Task<IActionResult> Update(int id)
        {
            return View(await _categoryService.Update(id));
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id,CategoryUpdateVM vM)
        {
            bool result = await _categoryService.UpdatePost(id,vM, ModelState);
            if (result) return RedirectToAction("index");
            return View(vM);
        }
        public  async Task<IActionResult> Delete(int id)
        {
            bool result = await _categoryService.Delete(id);
            if (result) return RedirectToAction("index");
            return View();
        }
    }
}
