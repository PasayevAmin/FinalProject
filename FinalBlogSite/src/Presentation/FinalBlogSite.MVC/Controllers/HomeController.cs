using FinalBlogSite.Application.Abstractions.Services;
using FinalBlogSite.Application.ViewModels;
using FinalBlogSite.Domain.Entities;
using FinalBlogSite.Persistence.DAL;
using FinalBlogSite.Persistence.Implementations.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FinalBlogSite.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAuthService _authService;
        private readonly IPostService _postService;

        public HomeController(AppDbContext context,UserManager<AppUser> userManager,IAuthService authService,IPostService postService)
        {
            _context = context;
            _userManager = userManager;
            _authService = authService;
            _postService = postService;
        }
        public async Task<IActionResult> Index()
        {
            AppUser appUser = null;
            if (User.Identity.IsAuthenticated)
            {
                appUser= await _userManager.FindByNameAsync(User.Identity.Name);


            }
            HomeVM vm = new HomeVM
            {
                LastestPost = await _context.Posts.Include(x => x.Category).Include(x => x.Author).Include(x=>x.Likes).OrderByDescending(x => x.CreatedAt).ToListAsync(),
                RecendPost = await _context.Posts.Include(x=>x.Category).Include(x=>x.Author).ToListAsync(),
                TitlePost=await _context.Posts.Include(x => x.Category).Include(x => x.Author).OrderBy(x=>x.Title).ToListAsync(),
                CategoryPost=await _context.Posts.Include(x=>x.Category).Include(x => x.Author).OrderBy(x=>x.CategoryId).Take(2).ToListAsync(),
                LikePost = await _context.Posts.Include(x => x.Category).Include(x => x.Author).OrderBy(x => x.LikeCount).Take(4).ToListAsync(),
                
                AppUser=appUser
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
            AppUser appUser = null;
            if (User.Identity.IsAuthenticated)
            {
                appUser = await _userManager.FindByNameAsync(User.Identity.Name);


            }
            AuthorProfileVM vM = new AuthorProfileVM
            {
                AuthorPost = await _context.Posts.Where(x => x.Author.UserName == User.Identity.Name).Include(x => x.Category).Include(x => x.Author).OrderByDescending(x => x.CreatedAt).ToListAsync(),
                Posts = await _postService.GetAllAsync(page, take),
                Follows = await _context.Folowers.ToListAsync(),
                Categories = await _context.Categories.Distinct().Include(x => x.Posts).ToListAsync(),
                AppUser = appUser
            };
             
            if (vM.Posts.Items == null) return NotFound();
            return View(vM);
        }
        public async Task<IActionResult> MemberProfile(int page = 1, int take = 4)
        {
            AppUser appUser = null;
            if (User.Identity.IsAuthenticated)
            {
                appUser = await _userManager.FindByNameAsync(User.Identity.Name);


            }
            ProfileVM vm = new ProfileVM
            {
                LastestPost = await _context.Posts.Include(x => x.Category).Include(x => x.Author).Include(x=>x.Likes).OrderByDescending(x => x.Id).ToListAsync(),
                RecendPost = await _context.Posts.Include(x => x.Category).Include(x => x.Author).ToListAsync(),
                CategoryPost = await _context.Posts.Include(x => x.Category).Include(x => x.Author).OrderBy(x => x.CategoryId).Take(2).ToListAsync(),
                Posts = await _postService.GetAllAsync(page, take),
                AppUsers = await _context.AppUsers.ToListAsync(),
                AppUser=appUser


            };
            return View(vm);
        }
        public async Task<IActionResult> ErrorPage(string error = "it stopped")
        {
            return View(model: error);
        }
        public IActionResult ContuctUs()
        {
            return View();
        }
        public async Task<IActionResult> Details(int id)
        {
            DetailsVM vM = new DetailsVM
            {

                Post = await _postService.GetPost(id)
            };

            return View(vM);
        }





    }
}