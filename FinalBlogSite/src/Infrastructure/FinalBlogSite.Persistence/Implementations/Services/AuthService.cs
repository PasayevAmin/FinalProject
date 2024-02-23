﻿using AutoMapper;
using FinalBlogSite.Application.Abstractions.Services;
using FinalBlogSite.Application.ViewModels.Account;
using FinalBlogSite.Application.ViewModels.Tokens;
using FinalBlogSite.Domain.Entities;
using FinalBlogSite.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using FinalBlogSite.Application.Abstractions.Extentions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.AspNetCore.Hosting;
using static System.Net.WebRequestMethods;
using Microsoft.AspNetCore.Http;
using FinalBlogSite.Application.Abstractions.Repositories;

namespace FinalBlogSite.Persistence.Implementations.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IFollowerRepository _followerRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _env;

        public AuthService(UserManager<AppUser> userManager,IFollowerRepository followerRepository,IHttpContextAccessor httpContextAccessor, IMapper mapper,SignInManager<AppUser> signInManager,RoleManager<IdentityRole> roleManager,IWebHostEnvironment env)
        {
            _userManager = userManager;
            _followerRepository = followerRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _env = env;
        }
        public async Task Follow(string followedId)
        {
            string userId = _httpContextAccessor.HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            AppUser followed = await _userManager.FindByIdAsync(followedId);
            
            followed.FollowerCount++;


            Follow foll = new Follow
            {

                FollowerId = userId
            };

            await _followerRepository.CreateAsync(foll);
            await _followerRepository.SaveChangesAsync();
        }
        public async Task<AppUser> GetUserById(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }
        public async Task Unfollow(string followedId)
        {
            string userId = _httpContextAccessor.HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            AppUser user = await _userManager.FindByIdAsync(userId);
            AppUser followed = await _userManager.FindByIdAsync(followedId);

            followed.FollowerCount--;

            Follow foll = await _followerRepository.GetSingleAsync(f => f.FollowerId == userId);

            if (foll != null)
            {
                _followerRepository.Delete(foll);
                await _followerRepository.SaveChangesAsync();
            }
        }
        public async Task<List<AppUser>> GetUsers(string searchTerm)
        {

            return await _userManager.Users.Where(x => x.UserName.ToLower().Contains(searchTerm.ToLower()) || x.FirstName.ToLower().Contains(searchTerm.ToLower()) || x.LastName.ToLower().Contains(searchTerm.ToLower())).ToListAsync();
        }


        public async Task<bool> RegisterAsync(RegisterVM vm,ModelStateDictionary modelstate)
        {
            //if (!modelstate.IsValid) return false;
            if (vm.IsDigitNumber(vm.Name))
            {
                modelstate.AddModelError("Name", "Name cannot contain numbers");
                return false;
            }
            if (vm.IsDigitNumber(vm.Surname))
            {
                modelstate.AddModelError("Surname", "Surname cannot contain numbers");
                return false;
            }


            if (!ValidateEmail(vm.Email, modelstate)) return false;
            if (!ValidateGender(vm.Gender, modelstate)) return false;

            var user = await CreateUserAsync(vm, modelstate);
            if (user == null) return false;

            var result = await _userManager.CreateAsync(user, vm.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    modelstate.AddModelError(String.Empty, item.Description);
                }
                return false;
            }
            await HandleUserRoleAsync(user, vm.Role);
            await _signInManager.SignInAsync(user, true);

            return true;
        }
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
            
        }
        public async Task CreateRoles()
        {
            foreach (var role in Enum.GetValues(typeof(UserRole)))
            {
                if (!await _roleManager.RoleExistsAsync(role.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole
                    {
                        Name = role.ToString(),
                    });
                }
            }
        }
        private bool ValidateEmail(string email, ModelStateDictionary modelstate)
        {
            if (!email.CheckEmail())
            {
                modelstate.AddModelError("EmailAdress", "Email is not entered correctly");
                return false;
            }
            return true;
        }
        private bool ValidateGender(Gender gender, ModelStateDictionary modelstate)
        {
            if (!Enum.IsDefined(typeof(Gender), gender))
            {
                modelstate.AddModelError("Gender", "Please select a valid gender");
                return false;
            }
            return true;
        }
        private async Task<AppUser> CreateUserAsync(RegisterVM vm, ModelStateDictionary modelstate)
        {
            var user = new AppUser
            {
                UserName = vm.Username.Trim(),
                FirstName = vm.Name.CapitalizeName(),
                LastName = vm.Surname.CapitalizeName(),
                Email = vm.Email,
                DateOfBirth = vm.DateOfBirthy,
                Gender= vm.Gender,
                Role = vm.Role,
                
            };

            if (vm.Photo != null)
            {
                if (!vm.Photo.CheckSize(10))
                {
                    modelstate.AddModelError("Photo", "Photo size incorrect");
                    return null;
                }
                if (!vm.Photo.CheckFile("image/"))
                {
                    modelstate.AddModelError("Photo", "Photo type incorrect");
                    return null;
                }
                string filename = await vm.Photo.CreateFileAsync(_env.WebRootPath, "assets", "img");
                user.ProfilePicture = filename;
            }
            return user;
        }
        private async Task HandleUserRoleAsync(AppUser user, UserRole role)
        {
            await _userManager.AddToRoleAsync(user, role.ToString());

            
        }

        public async Task<bool> LogInAsync(LogInVM vm, ModelStateDictionary modelstate)
        {
            if (!modelstate.IsValid) return false;
            AppUser user = await _userManager.FindByNameAsync(vm.UserNameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(vm.UserNameOrEmail);
                if (user is null)
                {
                    modelstate.AddModelError(string.Empty, "not found");
                    return false;
                }

            }
            var result = await _signInManager.PasswordSignInAsync(user, vm.Password, vm.IsRemembered, true);
            if (result.IsLockedOut)
            {
                modelstate.AddModelError(string.Empty, "Account is locked. Please try again after a few minutes.");
                return false;
            }
            if (!result.Succeeded)
            {
                modelstate.AddModelError(string.Empty, "password or email incorrect");
                return false;
            }
            user.IsActive = true;

            return true;
        }
        public async Task<AppUser> GetUserAsync(string userName)
        {
            return await _userManager.Users.Include(x => x.Posts).Include(x=>x.Comments).FirstOrDefaultAsync(x => x.UserName == userName);
        }

    }
}
