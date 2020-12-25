using Core.Entities.Base;
using Core.Enums;
using System;

namespace Core.Entities
{
    public class Operation : BaseEntity
    {
        public string Name { get; set; }
        public int OperationNumber { get; set; }     
        public OperationStatus Status { get; set; }
        public DateTime ExecutionTime { get; set; }
        public int ApplicationUserId { get; set; }
        public ApplicationUser User { get; set; }
        
    }
}
