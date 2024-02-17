using FinalBlogSite.Application.ViewModels;
using FinalBlogSite.Domain.Entities;
using FinalBlogSite.Persistence.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FinalBlogSite.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
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
        public async Task<IActionResult> Profile()
        {
            AuthorProfileVM vM = new AuthorProfileVM
            {
                AuthorPost = await _context.Posts.Where(x => x.Author.UserName == User.Identity.Name).Include(x=>x.Category).ToListAsync(),
                AppUser = await _context.AppUsers.ToListAsync(),

            };
            return View(vM);
        }
        public async Task<IActionResult> MemberProfile()
        {
            ProfileVM vm = new ProfileVM
            {
                LastestPost = await _context.Posts.Include(x => x.Category).Include(x => x.Author).OrderByDescending(x => x.Id).ToListAsync(),
                RecendPost = await _context.Posts.Include(x => x.Category).Include(x => x.Author).ToListAsync(),
                AppUser = await _context.AppUsers.ToListAsync(),
                CategoryPost = await _context.Posts.Include(x => x.Category).Include(x => x.Author).OrderBy(x => x.CategoryId).Take(2).ToListAsync(),



            };
            return View(vm);
        }
       



    }
}