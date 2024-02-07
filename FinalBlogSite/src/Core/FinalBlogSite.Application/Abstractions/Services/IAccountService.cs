using FinalBlogSite.Application.ViewModels.Account;
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
        Task<IdentityResult> RegisterAsync(RegisterVM model);
        Task<SignInResult> LoginAsync(LogInVM model);
        Task LogoutAsync();
    }
        
}
