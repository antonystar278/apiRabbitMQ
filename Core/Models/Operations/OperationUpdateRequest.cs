using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Models.Operations
{
    public class OperationUpdateRequest
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public DateTime ExecutionTIme { get; set; } 
    }
}
