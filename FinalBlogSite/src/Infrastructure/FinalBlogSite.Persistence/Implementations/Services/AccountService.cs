using FinalBlogSite.Application.Abstractions.Repositories;
using FinalBlogSite.Application.Abstractions.Repositories.Generic;
using FinalBlogSite.Application.Abstractions.Services;
using FinalBlogSite.Application.ViewModels.Account;
using FinalBlogSite.Domain.Entities;
using FinalBlogSite.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Persistence.Implementations.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _identityRepository;

        public AccountService(IAccountRepository identityRepository)
        {
            _identityRepository = identityRepository;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterVM vm)
        {

            AppUser user = new AppUser
            {
                FirstName = vm.Name,
                LastName = vm.Surname,
                Email = vm.Email,
                DateOfBirth = vm.DateOfBirthy,
                

            };
            return await _identityRepository.CreateUserAsync(user, vm.Password);

        }

        public async Task<SignInResult> LoginAsync(LogInVM model)
        {
            return await _identityRepository.SignInAsync(model.UserNameOrEmail, model.Password, model.IsRemembered);
        }

        public async Task LogoutAsync()
        {
              await _identityRepository.SignOutAsync();
        }

        public async Task<IdentityResult> CreateRoleAsync()
        {
            return await _identityRepository.CreateRole();
        }
    }
}
