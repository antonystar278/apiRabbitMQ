using Core.Entities;
using Core.Interfaces.Infrastructure;
using Core.Models.Operations;
using Infrastructure.AppContext;
using Infrastructure.Repositories.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Repositories.EFRepositories
{
    public class OperationRepository : BaseEfRepository<Operation>, IOperationRepository
    {
        public OperationRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task<OperationSummaryResponse> GetFilteredOperationsAsync(int pageSize, int pageIndex)
        {
            IQueryable<Operation> operations = _appDbContext.Operations;

            int count = await operations.CountAsync();

            var paginatedOperations = await PaginatedList<Operation>.CreateAsync(operations, pageIndex, pageSize);
            var response = new OperationSummaryResponse
            {
                Operations = paginatedOperations,
                TotalCount = count
            };
            return response;
        }
    }
}
