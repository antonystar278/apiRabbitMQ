using Core.Entities;
using Core.Interfaces.Infrastructure;
using Infrastructure.AppContext;
using Infrastructure.Repositories.Repositories.Base;

namespace Infrastructure.Repositories.Repositories.EFRepositories
{
    public class OperationRepository : BaseEfRepository<Operation>, IOperationRepository
    {
        public OperationRepository(AppDbContext appDbContext) : base(appDbContext) { }

        //public async Task<TResult> GetFilteredDataAsync<TEntity, TResult>(int pageSize, int pageIndex,
        //    Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> select)
        //{
        //    return await _appDbContext.Set<TEntity>().Where(predicate).Select(select);
        //}
    }
}
