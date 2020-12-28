using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Models.Operations
{
    public class OperationUpdateRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public TimeSpan ExecutionTime { get; set; } 
    }
}
