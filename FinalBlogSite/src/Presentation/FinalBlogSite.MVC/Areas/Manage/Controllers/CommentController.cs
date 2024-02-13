using FinalBlogSite.Application.Abstractions.Services;
using FinalBlogSite.Application.ViewModels;
using FinalBlogSite.Application.ViewModels.Categorys;
using FinalBlogSite.Application.ViewModels.Comment;
using FinalBlogSite.Domain.Entities;
using FinalBlogSite.Persistence.Implementations.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinalBlogSite.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        
        public async Task<IActionResult> Index(int page = 1, int take = 10)
        {
            PaginationVM<Comment> vm = await _commentService.GetAllAsync(page, take);
            if (vm.Items == null) return NotFound();
            return View(vm);
        }
        public async Task<IActionResult> Create()
        {
            CommentCreateVM vm = new CommentCreateVM();
            vm = await _commentService.CreatedAsync(vm);
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CommentCreateVM vm)
        {
            if (await _commentService.CreateAsync(vm, ModelState))
                return RedirectToAction(nameof(Index));
            return View(await _commentService.CreatedAsync(vm));
        }
        public async Task<IActionResult> Update(int id)
        {
            CommentUpdateVM vm = new CommentUpdateVM();
            vm = await _commentService.UpdatedAsync(id, vm);
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, CommentUpdateVM vm)
        {
            if (await _commentService.UpdateAsync(id, vm, ModelState))
                return RedirectToAction(nameof(Index));
            return View(await _commentService.UpdatedAsync(id, vm));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _commentService.DeleteAsync(id);
            if (result) return RedirectToAction(nameof(Index));
            return NotFound();
        }
    }
}
