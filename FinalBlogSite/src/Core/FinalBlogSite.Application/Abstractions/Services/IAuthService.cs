using FinalBlogSite.Application.ViewModels.Account;
using FinalBlogSite.Application.ViewModels.Tokens;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Application.Abstractions.Services
{
    public interface IAuthService
    {
        Task<bool> Register(RegisterVM dto,ModelStateDictionary modelstate);
        Task<bool> LogIn(LogInVM dto, ModelStateDictionary modelstate);
        Task Logout();
        Task CreateRoles();
    }
}
