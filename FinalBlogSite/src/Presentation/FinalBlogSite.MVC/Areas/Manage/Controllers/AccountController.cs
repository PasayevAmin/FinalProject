using FinalBlogSite.Application.Abstractions.Services;
using FinalBlogSite.Application.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;
using FinalBlogSite.Application.Abstractions.Extentions;
using FinalBlogSite.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using Humanizer;
using Microsoft.AspNetCore.SignalR;
using FinalBlogSite.Application.ViewModels.MailSender;
using NuGet.Protocol.Plugins;

namespace FinalBlogSite.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]

    public class AccountController : Controller
    {
        private readonly IAuthService _accountService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMailService _mailService;

        public AccountController(IAuthService accountService,UserManager<AppUser> userManager ,IMailService mailService)
        {
            _accountService = accountService;
            _userManager = userManager;
            _mailService = mailService;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register( RegisterVM vm)
        {
            
            if (!await _accountService.RegisterAsync(vm, ModelState))
            {
                return View(vm);

            }
            return RedirectToAction("index", "Home", new { Area = "" });

        }
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(LogInVM vM)
        {
            if (!await _accountService.LogInAsync(vM, ModelState))
            {
                return View(vM);

            }
            return Redirect(Request.Headers["Referer"]);





        }
        public async  Task<IActionResult> LogOut()
        {
            await _accountService.Logout();
            return RedirectToAction("index", "Home", new { Area = "" });


        }
        public async Task<IActionResult> CreateRole()
        {
            await _accountService.CreateRoles();
            return RedirectToAction("index", "Home", new { Area = "" });
        }
        public async Task<IActionResult> ForgotPassword()
        {
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM vM)
        {
            if (!ModelState.IsValid) return View(vM);

            var user = await _userManager.FindByEmailAsync(vM.Email);

            if (user == null) return NotFound();

            string token = await _userManager.GeneratePasswordResetTokenAsync(user);

            string link = Url.Action("ResetPassword", "Account", new { userID = user.Id, token = token }, HttpContext.Request.Scheme);

            await _mailService.SendEmailAsync(new MailRequestVM { ToEmail = vM.Email, Subject = "ResetPassword", Body = $"<a href='{link}'>ReserPassword</a>" });


            return RedirectToAction(nameof(LogIn));
        }
        public async Task<IActionResult> ResetPassword(string userId, string token)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token)) return BadRequest();
            var User = await _userManager.FindByIdAsync(userId);
            if (User == null) return NotFound();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM vm, string userId, string token)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token)) return BadRequest();
            if (!ModelState.IsValid) return View(vm);
            var User = await _userManager.FindByIdAsync(userId);
            if (User == null) return NotFound();
            var IdentityUser = await _userManager.ResetPasswordAsync(User, token, vm.ConfirmPassword);
            return RedirectToAction(nameof(LogIn));
        }
        public async Task<IActionResult> Follow(string followerId)
        {
            await _accountService.Follow(followerId);
            AppUser user = await _accountService.GetUserById(followerId);
            return RedirectToAction("Others", "Home", new { Area="",username = user.UserName });
        }
        public async Task<IActionResult> Unfollow(string followerId)
        {
            await _accountService.Unfollow(followerId);
            AppUser user = await _accountService.GetUserById(followerId);
            return RedirectToAction("Others", "Home", new {Area="", username = user.UserName });
        }
    }
       
    }



