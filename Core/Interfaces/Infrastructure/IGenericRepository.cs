using System;
using System.Threading.Tasks;

namespace Core.Interfaces.Infrastructure
{
    public interface IGenericRepository<T>
    {
        Task<T> GetByIdAsync(int id);
        Task<T> CreateAsync(T item);
        Task<T> UpdateAsync(T item);
    }

}
