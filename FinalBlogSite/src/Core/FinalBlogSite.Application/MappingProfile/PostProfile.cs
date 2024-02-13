﻿using AutoMapper;
using FinalBlogSite.Application.ViewModels.Categorys;
using FinalBlogSite.Application.ViewModels.Posts;
using FinalBlogSite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Application.MappingProfile
{
    public class PostProfile:Profile
    {
        public PostProfile()
        {
            CreateMap<PostCreateVM, Post>();
            CreateMap<PostUpdateVM, Post>().ReverseMap();
        }
    }
}
