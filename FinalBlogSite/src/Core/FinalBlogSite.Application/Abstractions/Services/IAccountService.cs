using FinalBlogSite.Application.ViewModels.Account;
using FinalBlogSite.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Application.Abstractions.Services
{
    public interface IAccountService
    {
        Task<IdentityResult> RegisterAsync(RegisterVM userVM);
        Task<SignInResult> LoginAsync(LogInVM userVM);
        Task<IdentityResult> CreateRoleAsync();
        Task LogoutAsync();
    }
        
}
