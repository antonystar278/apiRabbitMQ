using Ardalis.Specification;
using Core.Entities;
using Core.Models.Operations;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Interfaces.Infrastructure
{
    public interface IOperationRepository: IGenericRepository<Operation>
    {
        Task<OperationSummaryResponse> GetFilteredOperationsAsync(int pageSize, int pageIndex);
        Task<IReadOnlyList<Operation>> OperationListAsync(ISpecification<Operation> spec, CancellationToken cancellationToken = default);
    }
}
