using Core.Entities;
using System.Collections.Generic;

namespace Core.Models.Operations
{
    public class OperationSummaryResponse
    {
        public IList<Operation> Operations { get; set; } = new List<Operation>();
        public int TotalCount { get; set; }
    }
}
