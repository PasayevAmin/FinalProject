using FinalBlogSite.Application.ViewModels;
using FinalBlogSite.Application.ViewModels.Comment;
using FinalBlogSite.Application.ViewModels.Posts;
using FinalBlogSite.Application.ViewModels.Reply;
using FinalBlogSite.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Application.Abstractions.Services
{
    public  interface ICommentService
    {


        Task<List<string>> CreateComment(CommentCreateVM vm);
        Task<bool> DeleteAsync(int id);
        Task<PaginationVM<Comment>> GetAllAsync(int page = 1, int take = 3);
        Task<bool> UpdateAsync(int id, CommentUpdateVM vm, ModelStateDictionary modelstate);
        Task<CommentUpdateVM> UpdatedAsync(int id, CommentUpdateVM vm);
        Task<List<string>> CreateReply(CreateReplyVM vm);



    }
}
