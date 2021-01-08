using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Interfaces.Infrastructure
{
    public interface IGenericRepository<T>
    {
        Task<T> GetByIdAsync(int id);
        Task<T> CreateAsync(T item);
        Task<T> UpdateAsync(T item);
        //Task<TResult> GetFilteredDataAsync<TEntity, TResult>(int pageSize, int pageIndex,
        //    Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> select);
    }

}
