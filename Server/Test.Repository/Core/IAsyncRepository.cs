using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Test.Repository
{
    public interface IAsyncRepository<T> where T : class
    {

        Task<T> GetById(int id);
        Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate);

        Task<int> Add(T entity);
        Task<int> Update(T entity);
        Task<int> Remove(T entity);

        IQueryable<T> Query();
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> Where(Expression<Func<T, bool>> predicate);

        Task<int> CountAll();
        Task<int> CountWhere(Expression<Func<T, bool>> predicate);

    }
}