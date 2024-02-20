using AutoMapper;
using FinalBlogSite.Application.Abstractions.Extentions;
using FinalBlogSite.Application.Abstractions.Repositories;
using FinalBlogSite.Application.Abstractions.Services;
using FinalBlogSite.Application.ViewModels;
using FinalBlogSite.Application.ViewModels.Categorys;
using FinalBlogSite.Application.ViewModels;
using FinalBlogSite.Application.ViewModels.Posts;
using FinalBlogSite.Domain.Entities;
using FinalBlogSite.MVC.MiddleWears.Exseptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

namespace FinalBlogSite.Persistence.Implementations.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILikeRepository _likeRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ICategoryRepository _categoryRepository;
        private Dictionary<int, int> postLikes;

        public PostService(IPostRepository postRepository,UserManager<AppUser> userManager,ILikeRepository likeRepository,IMapper mapper,IHttpContextAccessor httpContextAccessor,IWebHostEnvironment webHostEnvironment,ICategoryRepository categoryRepository)
        {
            _postRepository = postRepository;
            _userManager = userManager;
            _likeRepository = likeRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _webHostEnvironment = webHostEnvironment;
            _categoryRepository = categoryRepository;
            postLikes = new Dictionary<int, int>();
        }
        public async Task<PaginationVM<Post>> GetAllAsync(int page = 1, int take = 3)
        {
            if (page < 1 || take < 1) throw new WrongRequestExceptions("Bad request");
            ICollection<Post> comments = await _postRepository.GetAllWhere(skip: (page - 1) * take, take: take, orderexpression: x => x.Id, isDescending: true).ToListAsync();
            if (comments == null) throw new NotFoundExceptions("Not Found");
            int count = await _postRepository.GetAll().CountAsync();
            if (count < 0) throw new NotFoundExceptions("Not Found");
            double totalpage = Math.Ceiling((double)count / take);
            PaginationVM<Post> vm = new PaginationVM<Post>
            {
                Items = comments.ToList(),
                CurrentPage = page,
                TotalPage = totalpage
            };
            return vm;
        }
        public async Task<bool> CreateAsync(PostCreateVM vm, ModelStateDictionary modelstate)
        {
            if (!modelstate.IsValid) return false;

            if (!await _categoryRepository.IsExist(s => s.Id == vm.CategoryId))
            {
                modelstate.AddModelError("CategoryId", "This category is not aviable");
                return false;
            }
            

            if (!vm.Photo.CheckSize(10))
            {
                modelstate.AddModelError("Photo", "Photo size incorrect");
                return false;
            }
            if (!vm.Photo.CheckFile("image/"))
            {
                modelstate.AddModelError("Photo", "Photo type incorrect");
                return false;
            }
            string filename = await vm.Photo.CreateFileAsync(_webHostEnvironment.WebRootPath, "assets", "img");
            Post post = new Post
            {
                Title = vm.Title.Trim(),

                Content = vm.Content.Trim(),
                LikeCount = 0,
                CreatedAt = DateTime.Now,
                CommentCount = 0,
                Images = filename,
                CategoryId = vm.CategoryId,

            };
            if (_httpContextAccessor.HttpContext.User.Identity != null)
            {
                post.AuthorId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            await _postRepository.AddAsync(post);
            await _postRepository.SaveChangesAsync();
            return true;
        }
        public async Task<PostCreateVM> CreatedAsync(PostCreateVM vm)
        {
            vm.Categories = await _categoryRepository.GetAll().ToListAsync();
            return vm;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            if (id < 1) throw new WrongRequestExceptions("Bad request");
            Post exist = await _postRepository.GetByIdAsync(id);
            if (exist == null) throw new NotFoundExceptions("not found");
            exist.Images.DeleteFile(_webHostEnvironment.WebRootPath, "assets", "img");
            _postRepository.Delete(exist);
            await _postRepository.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateAsync(int id, PostUpdateVM vm, ModelStateDictionary modelstate)
        {
            if (id < 0) throw new WrongRequestExceptions("Bad Request");
            if (!modelstate.IsValid) return false;
            Post exist = await _postRepository.GetByIdAsync(id);
            if (exist == null) throw new NotFoundExceptions("not found");

            if (!await _categoryRepository.IsExist(s => s.Id == vm.CategoryId))
            {
                modelstate.AddModelError("SubjectId", "This Subject is not aviable");
                return false;
            }
            exist.Title = vm.Title.Trim();
            exist.Content = vm.Content.Trim();
            exist.LikeCount = vm.LikeCount;
            exist.CategoryId = vm.CategoryId;
            exist.CommentCount=vm.CommentCount;
           

            _postRepository.Update(exist);
            await _postRepository.SaveChangesAsync();
            return true;
        }
        public async Task<PostUpdateVM> UpdatedAsync(int id, PostUpdateVM vm)
        {
            if (id < 0) throw new WrongRequestExceptions("Bad request");
            Post exist = await _postRepository.GetByIdAsync(id);
            if (exist == null) throw new NotFoundExceptions("not found");
            vm.Categories = await _categoryRepository.GetAll().ToListAsync();
            vm.Content = exist.Content.Trim();
            vm.LikeCount = exist.LikeCount;
            vm.CommentCount=exist.CommentCount;
            vm.CategoryId = exist.CategoryId;
            vm.Title = exist.Title.Trim();
            return vm;
        }
        public async Task<List<Post>> GetPosts()
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.Users
                .Include(u => u.Follows)
                .SingleOrDefaultAsync(u => u.Id == currentUserId);



            var followedUserIds = currentUser.Follows.Select(f => f.FollowerId).ToList();

            var posts = _postRepository.GetAll(p => followedUserIds.Contains(p.AuthorId) || p.AuthorId == currentUserId, nameof(Post.Author), nameof(Post.Comments), nameof(Post.Likes), "Comments.Replies").OrderByDescending(p => p.CreatedAt).ToList();

            return posts;

        }
        public async Task<Post> GetPost(int postId)
        {


            return await _postRepository.GetSingleAsync(x => x.Id == postId);

        }
       
        public async Task<List<Post>> GetMyPosts()
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.Users
                .SingleOrDefaultAsync(u => u.Id == currentUserId);

            var posts = _postRepository.GetAll(p => p.AuthorId == currentUserId, nameof(Post.Author), nameof(Post.Comments), nameof(Post.Likes)).OrderByDescending(p => p.CreatedAt).ToList();

            return posts;
        }
        public async Task<bool> LikePost(int postId)
        {
           
                var currentUserId = _httpContextAccessor.HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (currentUserId == null)
                {
                    
                    return false;
                }

                var currentUser = await _userManager.Users.Include(x => x.Likes)
                    .SingleOrDefaultAsync(u => u.Id == currentUserId);

                if (currentUser == null)
                {
                   
                    return false;
                }

                var existingLike = await _likeRepository.GetSingleAsync(l => l.PostId == postId && l.LikerId == currentUser.Id);
                if (existingLike != null)
                {
                    
                    return false;
                }

                var like = new Like
                {
                    LikerId = currentUser.Id,
                    PostId = postId
                };

                await _likeRepository.CreateAsync(like);

                var post = await _postRepository.GetSingleAsync(p => p.Id == postId);
                if (post == null)
                {
                   
                    return false;
                }

                post.LikeCount++;
                _postRepository.Update(post);

                await _postRepository.SaveChangesAsync();
                return true; 
            

        }
        public async Task<bool> UnlikePost(int postId)
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.Users
                .SingleOrDefaultAsync(u => u.Id == currentUserId);

            var existingLike = await _likeRepository.GetSingleAsync(l => l.PostId == postId && l.LikerId == currentUser.Id);

            if (existingLike != null)
            {
                _likeRepository.Delete(existingLike);

                var post = await _postRepository.GetSingleAsync(p => p.Id == postId);
                if (post != null && post.LikeCount > 0)
                {
                    post.LikeCount--;
                    _postRepository.Update(post);
                }

                await _likeRepository.SaveChangesAsync();
            }
            return true;
        }


        }
}
