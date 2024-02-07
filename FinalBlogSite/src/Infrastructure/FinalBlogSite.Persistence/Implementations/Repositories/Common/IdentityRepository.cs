using FinalBlogSite.Application.Abstractions.Repositories.Generic;
using FinalBlogSite.Domain.Entities;
using FinalBlogSite.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Persistence.Implementations.Repositories.Common
{
    public class IdentityRepository<T>:IIdentityRepository<T> where T :class,new()
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityRepository(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> AddToRole(AppUser user,UserRole role)
        {
            return await _userManager.AddToRoleAsync(user, role.ToString());
        }

        public async Task<IdentityResult> CreateRole()
        {
            foreach (UserRole item in Enum.GetValues(typeof(UserRole)))
            {
                    return await _roleManager.CreateAsync(new IdentityRole { Name = item.ToString() });

            }
            return await _roleManager.CreateAsync(new IdentityRole { Name="admin" });
        }

        public async Task<IdentityResult> CreateUserAsync(AppUser user, string password)
        {
            await _userManager.AddToRoleAsync(user,UserRole.Member.ToString());
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<SignInResult> SignInAsync(string username, string password, bool rememberMe)
        {
            return await _signInManager.PasswordSignInAsync(username, password, rememberMe, lockoutOnFailure: false);
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
