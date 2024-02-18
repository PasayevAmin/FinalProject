using FinalBlogSite.Application.Abstractions.Services;
using FinalBlogSite.Application.ViewModels;
using FinalBlogSite.Domain.Entities;
using FinalBlogSite.Persistence.DAL;
using FinalBlogSite.Persistence.Implementations.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FinalBlogSite.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IAuthService _authService;
        private readonly IPostService _postService;

        public HomeController(AppDbContext context,IAuthService authService,IPostService postService)
        {
            _context = context;
            _authService = authService;
            _postService = postService;
        }
        public async Task<IActionResult> Index()
        {
            HomeVM vm = new HomeVM
            {
                LastestPost = await _context.Posts.Include(x => x.Category).Include(x => x.Author).OrderByDescending(x => x.Id).ToListAsync(),
                RecendPost = await _context.Posts.Include(x=>x.Category).Include(x=>x.Author).ToListAsync(),
                TitlePost=await _context.Posts.Include(x => x.Category).Include(x => x.Author).OrderBy(x=>x.Title).ToListAsync(),
                CategoryPost=await _context.Posts.Include(x=>x.Category).Include(x => x.Author).OrderBy(x=>x.CategoryId).Take(2).ToListAsync(),
                LikePost = await _context.Posts.Include(x => x.Category).Include(x => x.Author).OrderBy(x => x.LikeCount).Take(4).ToListAsync(),


            };
            return View(vm);
        }
        public async Task<IActionResult> Search(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm) || searchTerm.Length < 3)
            {
                TempData["ErrorMessage"] = "Search term must be at least 3 characters long.";
                return RedirectToAction(nameof(Index));
            }
            List<AppUser> users = await _authService.GetUsers(searchTerm);
            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchPost(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm) || searchTerm.Length < 3)
            {
                TempData["ErrorMessage"] = "Search term must be at least 3 characters long.";
                return View("Search");
            }
            List<AppUser> users = await _authService.GetUsers(searchTerm);
            return View("Search", users);
        }
        public async Task<IActionResult> Profile(int page = 1,int take=4)
        {
            AuthorProfileVM vM = new AuthorProfileVM
            {
                AuthorPost = await _context.Posts.Where(x => x.Author.UserName == User.Identity.Name).Include(x=>x.Category).Include(x=>x.Author).ToListAsync(),
                //AppUser = await _context..ToListAsync(),
                Posts = await _postService.GetAllAsync(page, take),
                Follows=await _context.Folowers.ToListAsync(),
                
            };
             
            if (vM.Posts.Items == null) return NotFound();
            return View(vM);
        }
        public async Task<IActionResult> MemberProfile()
        {
            ProfileVM vm = new ProfileVM
            {
                LastestPost = await _context.Posts.Include(x => x.Category).Include(x => x.Author).OrderByDescending(x => x.Id).ToListAsync(),
                RecendPost = await _context.Posts.Include(x => x.Category).Include(x => x.Author).ToListAsync(),
                CategoryPost = await _context.Posts.Include(x => x.Category).Include(x => x.Author).OrderBy(x => x.CategoryId).Take(2).ToListAsync(),

                

            };
            return View(vm);
        }
        public async Task<IActionResult> ErrorPage(string error = "it stopped")
        {
            return View(model: error);
        }





    }
}