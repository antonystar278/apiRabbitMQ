using Core.Entities.Base.Interfaces;
using Core.Interfaces.Infrastructure;
using Infrastructure.AppContext;
using Microsoft.EntityFrameworkCore;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Ardalis.Specification.EntityFrameworkCore;

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

        public async Task<T> GetByIdAsync(int id)
        {
            return await _appDbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> UpdateAsync(T modifiedItem)
        {
            _appDbContext.Entry(modifiedItem).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
            return modifiedItem;
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec, CancellationToken cancellationToken = default)
        {
            var specificationResult = ApplySpecification(spec);
            return await specificationResult.ToListAsync(cancellationToken);
        }
        public async Task<int> CountAsync(ISpecification<T> spec, CancellationToken cancellationToken = default)
        {
            var specificationResult = ApplySpecification(spec);
            return await specificationResult.CountAsync(cancellationToken);
        }
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            var evaluator = new SpecificationEvaluator<T>();
            return evaluator.GetQuery(_appDbContext.Set<T>().AsQueryable(), spec);
        }
    }
}
