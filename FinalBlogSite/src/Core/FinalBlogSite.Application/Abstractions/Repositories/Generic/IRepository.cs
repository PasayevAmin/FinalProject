using FinalBlogSite.Domain.Entities;
using FinalBlogSite.Domain.Entities.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Application.Abstractions.Repositories.Generic
{
    public interface IRepository<T> where T : class, new()
    {
        IQueryable<T> GetAll(bool ignooreQuery = false, bool isTracking = false,
           params string[] includes);

        IQueryable<T> GetAllWhere(
            Expression<Func<T,
            bool>>? expression = null,
            Expression<Func<T, object>>? orderExpression = null,
            bool isDescending = false,
            int skip = 0,
            int take = 0,
            bool isTracking = false,
            bool ignoreQuery = false,
            params string[] includes);
        Task<T> GetByIdAsync(int id, bool isTracking = false, bool ignoreQuery = false, params string[] includes);
        Task<T> GetByExxpressionAsync(Expression<Func<T, bool>>? expression = null, bool isTracking = false, bool ignoreQuery = false, params string[] includes);
        Task<bool> IsExistedAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        void SoftDelete(T entity);
        void ReverseDelete(T entity);
        Task SaveChangesAsync();
        Task<IdentityResult> CreateUserAsync(AppUser user, string password);
        Task<SignInResult> SignInAsync(string username, string password, bool rememberMe);
        Task SignOutAsync();
    }
}
