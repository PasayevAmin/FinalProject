using FinalBlogSite.Application.Abstractions.Services;
using FinalBlogSite.Application.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;
using FinalBlogSite.Application.Abstractions.Extentions;
using FinalBlogSite.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using Humanizer;

namespace FinalBlogSite.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]

    public class AccountController : Controller
    {
        private readonly IAuthService _accountService;

        public AccountController(IAuthService accountService)
        {
            _accountService = accountService;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterVM vm)
        {
            var result = await _accountService.Register(vm, ModelState);
            if (result) return RedirectToAction("index", vm.Role.ToString());
            return View(vm);
        }
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(LogInVM vM,string returnurl)
        {
            await _accountService.LogIn(vM,ModelState);
            if (returnurl is null)
            {
                return RedirectToAction("index", "Home", new { Area = "" });

            }
            return Redirect(returnurl);



        }
        public async  Task<IActionResult> LogOut()
        {
            await _accountService.Logout();
            return RedirectToAction("Index", "Home", new { Area = "" });

        }
        public async Task<IActionResult> CreateRole()
        {
            await _accountService.CreateRoles();
            return RedirectToAction("index", "Home", new { Area = "" });
        }
    }
}
