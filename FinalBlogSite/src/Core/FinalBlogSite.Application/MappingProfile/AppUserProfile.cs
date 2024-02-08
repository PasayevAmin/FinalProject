using AutoMapper;
using FinalBlogSite.Application.ViewModels.Account;
using FinalBlogSite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Application.MappingProfile
{
    public class AppUserProfile : Profile
    {
        public AppUserProfile()
        {
            CreateMap<RegisterVM, AppUser>();
        }
    }
}
