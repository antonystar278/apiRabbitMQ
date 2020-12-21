using Core.Entities;
using Core.Models.Operations;
using System.Threading.Tasks;

namespace Core.Interfaces.Infrastructure
{
    public interface IOperationRepository: IGenericRepository<Operation>
    {
        Task<OperationSummaryResponse> GetFilteredOperationsAsync(int pageSize, int pageIndex);
    }
}
