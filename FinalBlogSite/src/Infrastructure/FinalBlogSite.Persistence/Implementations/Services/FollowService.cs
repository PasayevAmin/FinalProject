using AutoMapper;
using FinalBlogSite.Application.Abstractions.Repositories;
using FinalBlogSite.Application.Abstractions.Services;
using FinalBlogSite.Application.ViewModels;
using FinalBlogSite.Application.ViewModels.Follows;
using FinalBlogSite.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Persistence.Implementations.Services
{
    public class FollowService : IFollowService 
    {
        private readonly IFollowerRepository _followerRepository;
        private readonly IMapper _mapper;

        public FollowService(IFollowerRepository followerRepository,IMapper mapper)
        {
            _followerRepository = followerRepository;
            _mapper = mapper;
        }
        public async Task<bool> CreateAsync(CreateFollowVM vm,ModelStateDictionary modelstate)
        {
            if (!modelstate.IsValid)
            {
                return false;
            }
            if (await _followerRepository.IsExist(l => l.FollowerId == vm.FollowerId ))
            {
                modelstate.AddModelError("FollowerId", "This FollowerId is already exist");
                return false;
            }
            await _followerRepository.AddAsync(_mapper.Map<Follow>(vm));
            await _followerRepository.SaveChangesAsync();
            return true;
        }

        //public Task<bool> UpdateAsync(int id, UpdateFollowVM vm, ModelStateDictionary modelstate)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<UpdateFollowVM> UpdatedAsync(int id, UpdateFollowVM vm)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
