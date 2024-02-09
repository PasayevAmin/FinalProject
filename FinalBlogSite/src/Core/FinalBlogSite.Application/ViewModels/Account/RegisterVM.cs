using FinalBlogSite.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Application.ViewModels.Account
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "You can't send empty this value")]
        [MinLength(3, ErrorMessage = "Your name length must be bigger than 3")]
        [MaxLength(25, ErrorMessage = "Your name length must be less than 25")]
        public string Name { get; set; }
        [Required(ErrorMessage = "You can't send empty this value")]
        [MinLength(3, ErrorMessage = "Your Surname length must be bigger than 3")]
        [MaxLength(25, ErrorMessage = "Your Surname length must be less than 25")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "You can't send empty this value")]
        public Gender Gender { get; set; }
        [Required(ErrorMessage = "You can't send empty this value")]
        [MinLength(4, ErrorMessage = "Your Username length must be bigger than 4")]
        [MaxLength(100, ErrorMessage = "Your Username length must be less than 100")]
        public string Username { get; set; }
        [Required(ErrorMessage = "You can't send empty this value")]
        [DataType(DataType.EmailAddress)]

        public string Email { get; set; }
        [Required(ErrorMessage = "You can't send empty this value")]
        [MinLength(8, ErrorMessage = "Your password length must be bigger than 8")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "You can't send empty this value")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "You can't send empty this value")]
        public DateTime DateOfBirthy { get; set; }
        [Required]
        public UserRole Role { get; set; }
        public IFormFile Photo { get; set; }

    }
}
