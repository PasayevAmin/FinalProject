using AutoMapper;
using FinalBlogSite.Application.Abstractions.Repositories;
using FinalBlogSite.Application.Abstractions.Services;
using FinalBlogSite.Application.ViewModels;
using FinalBlogSite.Application.ViewModels.Categorys;
using FinalBlogSite.Application.ViewModels;
using FinalBlogSite.Application.ViewModels.Reply;
using FinalBlogSite.Domain.Entities;
using FinalBlogSite.MVC.MiddleWears.Exseptions;
using FinalBlogSite.Persistence.Implementations.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using Microsoft.AspNetCore.Identity;

namespace FinalBlogSite.Persistence.Implementations.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _comment;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IPostRepository _post;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IAuthService _authService;
        private readonly IReplyRepository _replyRepository;

        public CommentService(ICommentRepository comment,UserManager<AppUser> userManager,IMapper mapper,IPostRepository post,IHttpContextAccessor httpContext,IAuthService authService,IReplyRepository replyRepository)
        {
            _comment = comment;
            _userManager = userManager;
            _mapper = mapper;
            _post = post;
            _httpContext = httpContext;
            _authService = authService;
            _replyRepository = replyRepository;
        }
        public async Task<bool> AddComment(string content,int id)
        {
            var currentUserId = _httpContext.HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (currentUserId == null)
            {
                return false;
            }

            var currentUser = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == currentUserId);
            if (currentUser == null)
            {
                return false;
            }

            var post = await _post.GetSingleAsync(p => p.Id == id);
            if (post == null)
            {
                return false;
            }

            var comment = new Comment
            {
                AppUserId = currentUserId,
                PostId = id,
                Content = content,
                LikeCount=0,
                CreatedAt = DateTime.UtcNow 
            };

            await _comment.CreateAsync(comment);

            post.CommentCount++;
            _post.Update(post);

            await _post.SaveChangesAsync();
            return true;
        }




        public async Task<List<string>> CreateComment(CommentCreateVM vm)
        {
            List<string> str = new List<string>();
            Comment comment = _mapper.Map<Comment>(vm);
            comment.LikeCount = 0;
            Post post = await _post.GetByExpressionAsync(x => x.Id == vm.PostId, isDeleted: false);

            comment.AppUserId = _httpContext.HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            comment.PostId = vm.PostId;
            await _comment.CreateAsync(comment);
            await _comment.SaveChangesAsync();

            return str;
        }


        public async Task<List<string>> CreateReply(CreateReplyVM vm)
        {
            List<string> str = new List<string>();
            Reply reply = _mapper.Map<Reply>(vm);

            reply.AuthorId = _httpContext.HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            reply.RepliedCommentId = vm.CommentId;
            await _replyRepository.CreateAsync(reply);
            await _replyRepository.SaveChangesAsync();

            return str;
        }



        public async Task<bool> DeleteAsync(int id)
        {
            if (id < 0) throw new WrongRequestExceptions("Bad request");
            Comment exist = await _comment.GetByIdAsync(id);
            if (exist == null) throw new NotFoundExceptions("not found");
            _comment.Delete(exist);
            await _comment.SaveChangesAsync();
            return true;
        }

        public async Task<PaginationVM<Comment>> GetAllAsync(int page = 1, int take = 3)
        {
            if (page < 1 || take < 1) throw new WrongRequestExceptions("Bad request");
            ICollection<Comment> comments = await _comment.GetAllWhere(skip: (page - 1) * take, take: take, orderexpression: x => x.Id, isDescending: true).ToListAsync();
            if (comments == null) throw new NotFoundExceptions("Not Found");
            int count = await _comment.GetAll().CountAsync();
            if (count < 0) throw new NotFoundExceptions("Not Found");
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
            if (id < 0) throw new WrongRequestExceptions("Bad Request");
            Comment exist = await _comment.GetByIdAsync(id);
            if (exist == null) throw new NotFoundExceptions("not found");

            if (vm.Content != exist.Content)
            {
                ICollection<Comment> comments = await _comment.GetAllnotDeleted().ToListAsync();

                if (comments.Where(x => x.Content == vm.Content && x.PostId == vm.PostId).Count() >= 1)
                {
                    modelstate.AddModelError("Name", "You have this meal in your Meals");
                    return false;
                }

            }
            exist.Content = vm.Content;
            exist.LikeCount = vm.LikeCount;
            exist.CreatedAt = DateTime.UtcNow;
            exist.IsDeleted = false;
            

            _comment.Update(exist);
            await _comment.SaveChangesAsync();
            return true;
        }
        public async Task<CommentUpdateVM> UpdatedAsync(int id, CommentUpdateVM vm)
        {
            if (id < 0) throw new WrongRequestExceptions("Bad request");
            Comment exist = await _comment.GetByIdAsync(id);
            if (exist == null) throw new NotFoundExceptions("not found");
            vm.Posts = await _post.GetAll().ToListAsync();
            vm.Content = exist.Content.Trim();
            vm.LikeCount = exist.LikeCount;
            vm.PostId = exist.PostId;
            return vm;
        }
        public async Task<CommentIndexVM> GetComment(int postId)
        {
            CommentIndexVM commentIndex = new CommentIndexVM
            {
                Comments = await _comment.GetAll(x => x.Id == postId).Include(x=>x.Post).ToListAsync(),
            };


            return commentIndex;

        }
    }
}

