using FinalBlogSite.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Domain.Entities
{
    public class AppUser:IdentityUser
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? ProfilePicture { get; set; }
        public Gender Gender { get; set; }
        public UserRole Role { get; set; }
        public bool? IsActive { get; set; }
        public string? Facebook { get; set; }
        public string? Twitter { get; set; }

        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Followers> Followers { get; set; }
        public ICollection<Notification> Notifications { get; set; }








    }
}
