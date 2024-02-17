using AutoMapper;
using FinalBlogSite.Application.ViewModels.Comment;
using FinalBlogSite.Application.ViewModels.Follow;
using FinalBlogSite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Application.MappingProfile
{
    public class FollowProfile:Profile
    {
        public FollowProfile()
        {
            CreateMap<CreateFollowVM, Follow>();
            CreateMap<UpdateFollowVM, Follow>().ReverseMap();
        }
    }
}
