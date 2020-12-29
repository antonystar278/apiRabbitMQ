using System;

namespace Core.Models.Operations
{
    public class OperationModel
    {
        public int ApplicationUserId { get; set; }
        public int Id { get; set; }
        public TimeSpan ExecutionTime { get; set; }
    }
}
