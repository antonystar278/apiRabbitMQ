using Core.Entities;
using Core.Models.Operations;

namespace Core.Extensions.Operations
{
    public static class EntityToOperationCreateResponse
    {
        public static OperationCreateResponse ToDTO(this Operation operation)
        {
            var response = new OperationCreateResponse
            {
                Id = operation.Id,
                CreationDate = operation.CreationDate,
                Name = operation.Name,
                ExecutionTime = operation.ExecutionTime,
                OperationNumber = operation.OperationNumber,
                Status = operation.Status,
                ApplicationUserId = operation.ApplicationUserId
            };
            return response;
        }
    }
}
