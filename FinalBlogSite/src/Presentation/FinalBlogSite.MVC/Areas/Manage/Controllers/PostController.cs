using FinalBlogSite.Application.Abstractions.Services;
using FinalBlogSite.Application.ViewModels.Comment;
using FinalBlogSite.Application.ViewModels;
using FinalBlogSite.Domain.Entities;
using FinalBlogSite.Persistence.Implementations.Services;
using Microsoft.AspNetCore.Mvc;
using FinalBlogSite.Application.ViewModels.Posts;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace FinalBlogSite.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    //[Authorize(Roles = "Admin,Author")]

    public class PostController : Controller
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }
        [HttpPost]
        public async Task<IActionResult> LikedPost(int Id)
        {
            var likeResult = await _postService.LikePost(Id);
            if (!likeResult)
            {
                
                return BadRequest("Post cannot be liked.");
            }

            return Ok(); 
        }

        [HttpPost]
        public async Task<IActionResult> UnlikePost(int Id)
        {

            await _postService.UnlikePost(Id);
            var updatedPost = await _postService.GetPost(Id);
            var jsonOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
            };

            return Json(updatedPost, jsonOptions);
        }
        public async Task<IActionResult> Index(int page = 1, int take = 10)
        {
            PaginationVM<Post> vm = await _postService.GetAllAsync(page, take);
            if (vm.Items == null) return NotFound();
            return View(vm);
        }
        public async Task<IActionResult> Create()
        {
            PostCreateVM vm = new PostCreateVM();
            vm = await _postService.CreatedAsync(vm);
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Create(PostCreateVM vm)
        {
            if (await _postService.CreateAsync(vm, ModelState))
                return RedirectToAction("Profile", "Home", new {Area=""});
            return View(await _postService.CreatedAsync(vm));
        }
        public async Task<IActionResult> Update(int id)
        {
            PostUpdateVM vm = new PostUpdateVM();
            vm = await _postService.UpdatedAsync(id, vm);
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, PostUpdateVM vm)
        {
            if (await _postService.UpdateAsync(id, vm, ModelState))
                return RedirectToAction(nameof(Index));
            return View(await _postService.UpdatedAsync(id, vm));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _postService.DeleteAsync(id);
            if (result) return RedirectToAction(nameof(Index));
            return NotFound();
        }
        //[HttpPost]
        //public async Task<IActionResult> LikedPost(int postId)
        //{

        //    await _postService.LikePost(postId);
        //    var updatedPost = await _postService.GetPost(postId);
        //    var jsonOptions = new JsonSerializerOptions
        //    {
        //        ReferenceHandler = ReferenceHandler.Preserve,
        //    };

        //    return Json(updatedPost, jsonOptions);

        //}
        
    }
}
