using System.ComponentModel.DataAnnotations;

namespace Core.Models.Operations.Create
{
    public class OperationCreateRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
