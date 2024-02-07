using Microsoft.AspNetCore.Mvc;

namespace FinalBlogSite.MVC.Areas.Manage.Controllers
{
    public class AccountController : Controller
    {
        
        public IActionResult Register()
        {
            return View();
        }
    }
}
