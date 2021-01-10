using Core.Entities;
using Core.Models.Operations.ListPaged;
using System.Collections.Generic;

namespace Core.Models.Operations
{
    public class OperationSummaryResponse
    {
        public IList<OperationSummaryDTO> Operations { get; set; } = new List<OperationSummaryDTO>();
        public int TotalCount { get; set; }
    }
}
