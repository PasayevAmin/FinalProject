using FinalBlogSite.Application.ViewModels;
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
                LastestPost = await _context.Posts.OrderByDescending(x => x.Id).ToListAsync(),
                RecendPost = await _context.Posts.Include(x=>x.Category).ToListAsync(),
            };
            return View(vm);
        }
        public IActionResult Profile()
        {
            return View();
        }



    }
}