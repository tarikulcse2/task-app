using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Test.Entities.DBContext;

namespace Test.Repository
{
    public abstract class AsyncRepository<T> : IAsyncRepository<T> where T : class
    {

        protected DbContext Context;

        public AsyncRepository(DbContext context)
        {
            Context = context;
        }

        public Task<T> GetById(int id) => Context.Set<T>().FindAsync(id);

        public Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate)
            => Context.Set<T>().FirstOrDefaultAsync(predicate);

        public async Task<int> Add(T entity)
        {
            // await Context.AddAsync(entity);
            await Context.Set<T>().AddAsync(entity);
            return await Context.SaveChangesAsync();
        }

        public Task<int> Update(T entity)
        {
            // In case AsNoTracking is used
            Context.Entry(entity).State = EntityState.Modified;
            return Context.SaveChangesAsync();
        }

        public Task<int> Remove(T entity)
        {
            Context.Set<T>().Remove(entity);
            return Context.SaveChangesAsync();
        }

        public IQueryable<T> Query() {
            return Context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await Context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> Where(Expression<Func<T, bool>> predicate)
        {
            return await Context.Set<T>().Where(predicate).ToListAsync();
        }

        public Task<int> CountAll() => Context.Set<T>().CountAsync();

        public Task<int> CountWhere(Expression<Func<T, bool>> predicate) 
            => Context.Set<T>().CountAsync(predicate);

    }
}