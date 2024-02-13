using AutoMapper;
using FinalBlogSite.Application.ViewModels.Categorys;
using FinalBlogSite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Application.MappingProfile
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryCreateVM, Category>();
            CreateMap<CategoryUpdateVM, Category>().ReverseMap();
        }

    }
}
