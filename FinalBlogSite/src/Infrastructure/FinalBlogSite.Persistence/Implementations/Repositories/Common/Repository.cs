using FinalBlogSite.Application.Abstractions.Repositories.Generic;
using FinalBlogSite.Domain.Entities;
using FinalBlogSite.Persistence.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Persistence.Implementations.Repositories.Common
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _table;
        public Repository(AppDbContext context)
        {
            _context = context;
            _table = context.Set<T>();
        }
        public Task AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetAll(bool ignooreQuery = false, bool isTracking = false, params string[] includes)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetAllWhere(Expression<Func<T, bool>>? expression = null, Expression<Func<T, object>>? orderExpression = null, bool isDescending = false, int skip = 0, int take = 0, bool isTracking = false, bool ignoreQuery = false, params string[] includes)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByExxpressionAsync(Expression<Func<T, bool>>? expression = null, bool isTracking = false, bool ignoreQuery = false, params string[] includes)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(int id, bool isTracking = false, bool ignoreQuery = false, params string[] includes)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExistedAsync(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public void ReverseDelete(T entity)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

       

      

        public void SoftDelete(T entity)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
       
    
}
}
