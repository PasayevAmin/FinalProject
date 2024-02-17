using FinalBlogSite.Application.ViewModels;
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
        IQueryable<T> GetAll(Expression<Func<T, bool>>? expression = null, params string[] includes);
        IQueryable<T> GetAllWhere(Expression<Func<T, bool>>? expression = null,
            Expression<Func<T, object>>? orderexpression = null,
            bool isDescending = false,
            bool isTracking = false,
            bool queryFilter = false,
            int skip = 0,
            int take = 0,
            params string[] includes);
        Task<bool> IsExist(Expression<Func<T, bool>> expression);
        Task<T> GetByIdAsync(int id, bool isTracking = false, bool queryFilter = false, params string[] includes);
        Task<T> GetByExpressionAsync(Expression<Func<T, bool>> expression, bool? isDeleted = false, bool isTracking = false, bool queryFilter = false, params string[] includes);
        Task AddAsync(T entity);
        void Delete(T entity);
        void Update(T entity);
        Task SaveChangesAsync();
        void Includes(T entity, params string[] includes);
        Task<ICollection<E>> GetEntity<E>() where E : class;
        IQueryable<T> GetAllnotDeleted(bool isTracking = false, params string[] includes);
        Task CreateAsync(T entity);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> expression, params string[] includes);


    }
}
