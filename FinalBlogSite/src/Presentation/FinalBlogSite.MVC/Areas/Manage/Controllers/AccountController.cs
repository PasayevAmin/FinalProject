using FinalBlogSite.Application.Abstractions.Services;
using FinalBlogSite.Application.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;
using FinalBlogSite.Application.Abstractions.Extentions;
using FinalBlogSite.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;

namespace FinalBlogSite.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]

    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM vM)
        {
            await _accountService.RegisterAsync(vM);
            if (!ModelState.IsValid) return View();
            if (!vM.CheckWords(vM.Name) || !vM.IsSymbol(vM.Name) || !vM.IsDigit(vM.Name))
            {
                ModelState.AddModelError("Name", "your name mustn't contain space,numbers and symbol");
                return View();
            }
            if (!vM.CheckWords(vM.Surname) || !vM.IsSymbol(vM.Surname) || !vM.IsDigit(vM.Surname))
            {
                ModelState.AddModelError("Surname", "your Surname mustn't contain space,numbers and symbol");
                return View();
            }

            if (!vM.CheckEmail(vM.Email))
            {
                ModelState.AddModelError("Email", "your email type isn't true please try again");
                return View();
            }
            await _accountService.RegisterAsync(vM);
            return RedirectToAction("Index", "Home",new {Area=""});

        }
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(LogInVM vM,string returnurl)
        {
           var value= await _accountService.LoginAsync(vM);
            if (value.IsLockedOut)
            {
                ModelState.AddModelError(String.Empty, "You are Bloced");
                return View();
            }
            if (!value.Succeeded)
            {
                ModelState.AddModelError(String.Empty, "Username,Email or Password is incorrect");
                return View();
            }
            if (returnurl is null)
            {
                return RedirectToAction("Index", "Home", new { Area = "" });
            }
            return Redirect(returnurl);


        }
        public async  Task<IActionResult> LogOut()
        {
            await _accountService.LogoutAsync();
            return RedirectToAction("Index", "Home", new { Area = "" });

        }
        public async Task<IActionResult> CreateRole()
        {
            await _accountService.CreateRoleAsync();
            return RedirectToAction("Index", "Home", new { Area = "" });


        }
    }
}
