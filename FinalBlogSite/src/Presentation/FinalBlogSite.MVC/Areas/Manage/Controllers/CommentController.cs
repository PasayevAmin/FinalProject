using FinalBlogSite.Application.Abstractions.Services;
using FinalBlogSite.Application.ViewModels;
using FinalBlogSite.Application.ViewModels.Categorys;
using FinalBlogSite.Application.ViewModels.Comment;
using FinalBlogSite.Application.ViewModels.Reply;
using FinalBlogSite.Domain.Entities;
using FinalBlogSite.Persistence.Implementations.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinalBlogSite.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IPostService _postService;

        public CommentController(ICommentService commentService, IPostService postService)
        {
            _commentService = commentService;
            _postService = postService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(CommentCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Home/Index.cshtml", new HomeVM { CreateCommentVM = vm, Posts = await _postService.GetPosts() });
            }

            var res = await _commentService.CreateComment(vm);
            if (res.Any())
            {
                throw new Exception();
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> CreateReply(CreateReplyVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Home/Index.cshtml", new HomeVM { CreateReplyVM = vm, Posts = await _postService.GetPosts() });
            }

            var res = await _commentService.CreateReply(vm);
            if (res.Any())
            {
                throw new Exception();
            }
            return RedirectToAction("Index", "Home");
        }
    }

    }
