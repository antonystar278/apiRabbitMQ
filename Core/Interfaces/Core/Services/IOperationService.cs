using Core.Models.Operations;
using Core.Models.Operations.Create;
using Core.Models.Operations.ListPaged;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Interfaces.Core.Services
{
    public interface IOperationService
    {
        Task<OperationCreateResponse> CreateAsync(OperationCreateRequest request);
        Task UpdateAsync(OperationCreateMessageModel operationModel);
        Task<ListPagedOperationsResponse> GetFilteredDataAsync(ListPagedOperationsRequest request, CancellationToken cancellationToken);
    }
}
 