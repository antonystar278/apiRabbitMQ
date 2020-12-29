using Core.Models.Operations;
using System.Threading.Tasks;

namespace Core.Interfaces.Core.Services
{
    public interface IOperationService
    {
        Task<OperationCreateResponse> CreateAsync(OperationCreateRequest request);
        Task UpdateAsync(OperationModel operationModel);
        Task<OperationSummaryResponse> GetFilteredOperationsAsync(int pageSize, int pageIndex);
    }
}
 