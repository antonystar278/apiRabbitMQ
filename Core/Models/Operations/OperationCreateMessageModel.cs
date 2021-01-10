using System;

namespace Core.Models.Operations
{
    public class OperationCreateMessageModel
    {
        public int ApplicationUserId { get; set; }
        public int Id { get; set; }
        public TimeSpan ExecutionTime { get; set; }
    }
}
