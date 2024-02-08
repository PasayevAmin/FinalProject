using FinalBlogSite.Application.ViewModels.Tokens;
using FinalBlogSite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Application.Abstractions.Services
{
    public interface IJwtService
    {
        TokenResponseVM CreateToken(AppUser user, int minutes);
    }
}
