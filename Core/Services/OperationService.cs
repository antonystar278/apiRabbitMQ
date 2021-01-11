using Core.Entities;
using Core.Enums;
using Core.Extensions.Operations;
using Core.Interfaces.Core.Services;
using Core.Interfaces.Infrastructure;
using Core.Interfaces.Operations.Messaging.Send;
using Core.Models.Operations;
using Core.Models.Operations.Create;
using Core.Models.Operations.ListPaged;
using Core.Specifications;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Services
{
    public class OperationService : IOperationService
    {
        protected readonly IOperationRepository _operationRepository;
        private readonly IOperationUpdateSender _operationUpdateSender;

        public OperationService(IOperationRepository operationRepository,
            IOperationUpdateSender operationUpdateSender
)
        {
            _operationRepository = operationRepository;
            _operationUpdateSender = operationUpdateSender;

        }
        public async Task<OperationCreateResponse> CreateAsync(OperationCreateRequest request)
        {
            var entity = new Operation
            {
                CreationDate = DateTime.Now,
                Name = request.Name,
                Status = OperationStatus.InProcess,
                ApplicationUserId = request.UserId
            };

            Operation operation = await _operationRepository.CreateAsync(entity);
            await SendOperationAsync(operation);

            return entity.ToDTO();
        }
        private async Task SendOperationAsync(Operation operation)
        {
            var messageModel = new OperationCreateMessageModel
            {
                Id = operation.Id,
                ExecutionTime = operation.ExecutionTime,
                ApplicationUserId = operation.ApplicationUserId
            };

            await _operationUpdateSender.SendOperationToQueue(messageModel);
        }

        public async Task UpdateAsync(OperationCreateMessageModel operationModel)
        {
            Operation operation = await _operationRepository.GetByIdAsync(operationModel.Id);
            operation.ExecutionTime = operationModel.ExecutionTime;
            operation.Status = OperationStatus.Completed;
            await _operationRepository.UpdateAsync(operation);
        }

        public async Task<ListPagedOperationsResponse> GetFilteredDataAsync(ListPagedOperationsRequest request, CancellationToken cancellationToken)
        {
            var response = new ListPagedOperationsResponse();
            var filterSpec = new OperationFilterSpecification();

            response.TotalCount = await _operationRepository.CountAsync(filterSpec, cancellationToken);

            var pagedSpec = new OperationFilterPaginatedSpecification(
                skip: request.PageIndex * request.PageSize,
                take: request.PageSize);

            var operations = await _operationRepository.ListAsync(pagedSpec, cancellationToken);

            response.Operations = operations.Select(operation => new OperationSummaryDTO
            {
                Id = operation.Id,
                Name = operation.Name,
                UserName = operation.User.UserName,
                CreationDate = operation.CreationDate,
                Status = operation.Status,
                ExecutionTime = operation.ExecutionTime
            }).ToList();

            return response;
        }
    }
}
