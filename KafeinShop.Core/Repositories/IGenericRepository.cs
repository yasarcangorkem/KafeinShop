﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KafeinShop.Core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        IQueryable<T> GetAll();
        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
        //IQueryable<T> Where(Expression<Func<T, bool>> expression);
        //Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        //Task AddRangeAsync(IEnumerable<T> entities);
        //void RemoveRange(IEnumerable<T> entities);
    }
}
