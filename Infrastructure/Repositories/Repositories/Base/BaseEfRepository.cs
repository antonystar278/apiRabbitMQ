using Core.Entities.Base.Interfaces;
using Core.Interfaces.Infrastructure;
using Infrastructure.AppContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Repositories.Base
{
    public abstract class BaseEfRepository<T> : IGenericRepository<T> where T : class, IBaseEntity
    {
        protected readonly AppDbContext _appDbContext;
        public BaseEfRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<T> CreateAsync(T item)
        {
            await _appDbContext.Set<T>().AddAsync(item);
            await _appDbContext.SaveChangesAsync();
            return item;
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _appDbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> UpdateAsync(T modifiedItem)
        {
            _appDbContext.Entry(modifiedItem).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
            T item = await GetByIdAsync(modifiedItem.Id);
            return item;

        }
    }
}
