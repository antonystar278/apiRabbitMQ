using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Interfaces.Infrastructure
{
    public interface IGenericRepository<T>
    {
        Task<T> GetByIdAsync(int id);
        Task<T> CreateAsync(T item);
        Task<T> UpdateAsync(T item);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);
        Task<int> CountAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);
        //Task<TResult> GetFilteredDataAsync<TEntity, TResult>(int pageSize, int pageIndex,
        //    Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> select);
    }
}
