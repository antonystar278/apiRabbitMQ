using Core.Enums;
using System;

namespace Core.Models.Operations
{
    public class OperationSummaryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public DateTime CreationDate { get; set; }
        public OperationStatus Status { get; set; }
        public TimeSpan ExecutionTime { get; set; }
    }
}
