using FinalBlogSite.Application.Abstractions.Repositories;
using FinalBlogSite.Domain.Entities;
using FinalBlogSite.Persistence.DAL;
using FinalBlogSite.Persistence.Implementations.Repositories.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Persistence.Implementations.Repositories
{
    public class AccountRepository: IdentityRepository<AppUser>, IAccountRepository
    {
        public AccountRepository(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,RoleManager<IdentityRole> roleManager):base(userManager, signInManager,roleManager) 
        {
            
        }
    }
}
