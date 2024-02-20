using FinalBlogSite.Application.ViewModels;
using FinalBlogSite.Application.ViewModels.Follows;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Application.Abstractions.Services
{
    public interface IFollowService
    {
        Task<bool> CreateAsync(CreateFollowVM vm,ModelStateDictionary keyValuePairs);
        //Task<bool> UpdateAsync(int id, UpdateFollowVM vm, ModelStateDictionary modelstate);
        //Task<UpdateFollowVM> UpdatedAsync(int id, UpdateFollowVM vm);
    }
}
