using FinalBlogSite.Domain.Entities;
using FinalBlogSite.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Application.Abstractions.Repositories.Generic
{
    public interface IIdentityRepository<T> where T : class, new()
    {
        Task<IdentityResult> CreateUserAsync(AppUser user, string password);
        Task<SignInResult> SignInAsync(string username, string password, bool rememberMe);
        Task<IdentityResult> CreateRole();
        Task<IdentityResult> AddToRole(AppUser user,UserRole role);


        Task SignOutAsync();
    }
}
