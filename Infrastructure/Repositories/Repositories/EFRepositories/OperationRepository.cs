using Core.Entities;
using Core.Interfaces.Infrastructure;
using Core.Models.Operations;
using Infrastructure.AppContext;
using Infrastructure.Repositories.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

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


        public async Task<OperationSummaryResponse> GetFilteredOperationsAsync(int pageSize, int pageIndex)
        {
            IQueryable<OperationSummaryDTO> operations = _appDbContext.Operations
                .Include(x => x.User)
                .Select(operation => new OperationSummaryDTO
                {
                    Id = operation.Id,
                    Name = operation.Name,
                    UserName = operation.User.UserName,
                    CreationDate = operation.CreationDate,
                    Status = operation.Status,
                    ExecutionTime = operation.ExecutionTime
                });

            int count = await operations.CountAsync();

            var paginatedOperations = await PaginatedList<OperationSummaryDTO>.CreateAsync(operations, pageIndex, pageSize);
            var response = new OperationSummaryResponse
            {
                Operations = paginatedOperations,
                TotalCount = count
            };
            return response;
        }
    }
}
