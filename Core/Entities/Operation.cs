using Core.Entities.Base;
using Core.Entities.Enums;
using System;

namespace Core.Entities
{
    public class Operation : BaseEntity
    {
        public int OperationNumber { get; set; }
        public Guid UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public OperationStatus Status { get; set; }
        public DateTime ExecutionTime { get; set; }
    }
}
