using AutoMapper;
using FinalBlogSite.Application.Abstractions.Repositories;
using FinalBlogSite.Application.Abstractions.Services;
using FinalBlogSite.Application.ViewModels;
using FinalBlogSite.Application.ViewModels.Categorys;
using FinalBlogSite.Application.ViewModels.Comment;
using FinalBlogSite.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Persistence.Implementations.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _comment;
        private readonly IMapper _mapper;
        private readonly IPostRepository _post;

        public CommentService(ICommentRepository comment,IMapper mapper,IPostRepository post)
        {
            _comment = comment;
            _mapper = mapper;
            _post = post;
        }
        public async Task<bool> CreateAsync(CommentCreateVM vm, ModelStateDictionary modelstate)
        {
            if (!modelstate.IsValid) return false;
           
            if (!await _post.IsExist(s => s.Id == vm.PostId))
            {
                modelstate.AddModelError("SubjectId", "This Subject is not aviable");
                return false;
            }
           
            Comment comment = new Comment
            {
                Content = vm.Content.Trim(),
                LikeCount = vm.LikeCount,
                CreatedAt =DateTime.Now,
                PostId = vm.PostId,
                
            };
            await _comment.AddAsync(comment);
            await _comment.SaveChangesAsync();
            return true;
        }

        public async Task<CommentCreateVM> CreatedAsync(CommentCreateVM vm)
        {
            vm.Posts = await _post.GetAll().ToListAsync();
            return vm;
        }




        public async Task<bool> DeleteAsync(int id)
        {
            if (id < 1) throw new Exception("Bad request");
            Comment exist = await _comment.GetByIdAsync(id);
            if (exist == null) throw new Exception("not found");
            _comment.Delete(exist);
            await _comment.SaveChangesAsync();
            return true;
        }

        public async Task<PaginationVM<Comment>> GetAllAsync(int page = 1, int take = 3)
        {
            if (page < 1 || take < 1) throw new Exception("Bad request");
            ICollection<Comment> comments = await _comment.GetAllWhere(skip: (page - 1) * take, take: take, orderexpression: x => x.Id, isDescending: true).ToListAsync();
            if (comments == null) throw new Exception("Not Found");
            int count = await _comment.GetAll().CountAsync();
            if (count < 0) throw new Exception("Not Found");
            double totalpage = Math.Ceiling((double)count / take);
            PaginationVM<Comment> vm = new PaginationVM<Comment>
            {
                Items = comments.ToList(),
                CurrentPage = page,
                TotalPage = totalpage
            };
            return vm;
        }

        public async Task<bool> UpdateAsync(int id, CommentUpdateVM vm, ModelStateDictionary modelstate)
        {
            if (id < 0) throw new Exception("Bad Request");
            if (!modelstate.IsValid) return false;
            Comment exist = await _comment.GetByIdAsync(id);
            if (exist == null) throw new Exception("not found");
           
            if (!await _post.IsExist(s => s.Id == vm.PostId))
            {
                modelstate.AddModelError("SubjectId", "This Subject is not aviable");
                return false;
            }
            
            exist.Content = vm.Content.Trim();
            exist.LikeCount = vm.LikeCount;
            exist.PostId = vm.PostId;
            
            _comment.Update(exist);
            await _comment.SaveChangesAsync();
            return true;
        }
        public async Task<CommentUpdateVM> UpdatedAsync(int id, CommentUpdateVM vm)
        {
            if (id < 0) throw new Exception("Bad request");
            Comment exist = await _comment.GetByIdAsync(id);
            if (exist == null) throw new Exception("not found");
            vm.Posts = await _post.GetAll().ToListAsync();
            vm.Content = exist.Content.Trim();
            vm.LikeCount = exist.LikeCount;
            vm.PostId = exist.PostId;
            return vm;
        }
    }
}

