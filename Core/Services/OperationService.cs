using Core.Entities;
using Core.Enums;
using Core.Interfaces.Core.Services;
using Core.Interfaces.Infrastructure;
using Core.Interfaces.Operations.Messaging.Send;
using Core.Models.Operations;
using System;
using System.Collections.Generic;
using System.Text;
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
        public async Task<Operation> CreateAsync(OperationCreateRequest request)
        {
            var entity = new Operation
            {
                CreationDate = DateTime.Now,
                Name = request.Name,
                Status = OperationStatus.NotStarted,
                ApplicationUserId = request.UserId
            };

            await _operationRepository.CreateAsync(entity);

            await _operationUpdateSender.SendOperation(entity);

            return entity;
        }

        public async Task<OperationSummaryResponse> GetFilteredOperationsAsync(int pageSize, int pageIndex)
        {
            OperationSummaryResponse response = await _operationRepository.GetFilteredOperationsAsync(pageSize, pageIndex);
            return response;
        }

        public async Task UpdateAsync(Operation updatedOperation)
        {
            Operation operation = await _operationRepository.GetByIdAsync(updatedOperation.Id);
            operation.ExecutionTime = updatedOperation.ExecutionTime;
            await _operationRepository.UpdateAsync(operation);
        }
    }
}
