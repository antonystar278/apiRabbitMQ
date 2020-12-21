using Core.Entities;
using Core.Models.Operations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Core.Services
{
    public interface IOperationService
    {
        Task<Operation> CreateAsync(OperationCreateRequest request);
        Task<Operation> UpdateAsync(OperationUpdateRequest request);
        Task<OperationSummaryResponse> GetFilteredOperationsAsync(int pageSize, int pageIndex);
    }
}
 