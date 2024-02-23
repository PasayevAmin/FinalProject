using FinalBlogSite.Application.Abstractions.Services;
using FinalBlogSite.Application.ViewModels;
using FinalBlogSite.Application.ViewModels.Categorys;
using FinalBlogSite.Application.ViewModels;
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
        public async Task<IActionResult> Index(int id)
        {
              await _commentService.GetComment(id);
            return View();
        }
        public async Task<IActionResult> AddComment(string content,int postid)
        {
           

            var success = await _commentService.AddComment(content,postid);
            if (success)
            {
                return RedirectToAction("Details", "Home",new {Area="",Id=postid}); 
            }
            else
            {
                return RedirectToAction("ErrorPage","Home",new {Area=""}); 
            }
        }

        public async Task<IActionResult> Create(CommentCreateVM vm,string returnurl)
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
            if (returnurl is null)
            {
                return RedirectToAction("Index", "Home",new {Area=""});

            }
            return Redirect(returnurl);
        }
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
