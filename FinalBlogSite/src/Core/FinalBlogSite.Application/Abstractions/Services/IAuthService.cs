using FinalBlogSite.Application.ViewModels.Account;
using FinalBlogSite.Application.ViewModels.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Application.Abstractions.Services
{
    public interface IAuthService
    {
        Task Register(RegisterVM dto);
        Task LogIn(LogInVM dto);
        Task Logout();
        Task CreateRoles();
    }
}
