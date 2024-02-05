using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FinalBlogSite.MVC.Controllers
{
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Profile()
        {
            return View();
        }



    }
}