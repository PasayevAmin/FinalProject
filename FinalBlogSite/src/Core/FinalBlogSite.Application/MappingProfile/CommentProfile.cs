﻿using AutoMapper;
using FinalBlogSite.Application.ViewModels.Categorys;
using FinalBlogSite.Application.ViewModels.Comment;
using FinalBlogSite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Application.MappingProfile
{
    public class CommentProfile:Profile
    {
        public CommentProfile()
        {
            CreateMap<CommentCreateVM, Comment>();
            CreateMap<CommentUpdateVM, Comment>().ReverseMap();
        }
    }
}