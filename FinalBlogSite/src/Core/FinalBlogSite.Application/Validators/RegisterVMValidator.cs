using FinalBlogSite.Application.ViewModels.Account;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Application.Validators
{
    public class RegisterVMValidator:AbstractValidator<RegisterVM>
    {
        public RegisterVMValidator()
        {
            //RuleFor(x => x.Email).NotEmpty().Matches(@$"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$").MaximumLength(256);
            //RuleFor(x => x.Name).NotEmpty().WithMessage("Name Is Reguired")
            //    .MaximumLength(100).WithMessage("Name must consist 100 caracters")
            //    .MinimumLength(3).WithMessage("Name must be more than 3 characters");
            //RuleFor(x => x.Password).NotEmpty().MinimumLength(8).MaximumLength(100);
            //RuleFor(x => x).Must(x => x.ConfirmPassword == x.Password);
            //RuleFor(x => x.Username).NotEmpty().MinimumLength(4).MaximumLength(256);
            //RuleFor(x => x.Name).NotEmpty().MinimumLength(3).MaximumLength(50);
            //RuleFor(x => x.Surname).NotEmpty().MinimumLength(4).MaximumLength(256);
        }
    }
    
}
