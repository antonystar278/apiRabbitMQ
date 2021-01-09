using Core.Enums;
using System;

namespace Core.Models.Operations.Create
{
    public class OperationCreateResponse
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Name { get; set; }
        public int OperationNumber { get; set; }
        public OperationStatus Status { get; set; }
        public TimeSpan ExecutionTime { get; set; }
        public int ApplicationUserId { get; set; }
    }
}
