using System.Collections.Generic;

namespace Core.Models.Operations.ListPaged
{
    public class ListPagedOperationsResponse
    {
        public IList<OperationSummaryDTO> Operations { get; set; } = new List<OperationSummaryDTO>();
        public int TotalCount { get; set; }
    }
}
