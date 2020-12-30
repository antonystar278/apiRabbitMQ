using Core.Entities;
using Core.Enums;
using Core.Extensions.Operations;
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
        public async Task<OperationCreateResponse> SendCreateAsync(OperationCreateRequest request)
        {
            var entity = new Operation
            {
                CreationDate = DateTime.Now,
                Name = request.Name,
                Status = OperationStatus.InProcess,
                ApplicationUserId = request.UserId
            };

            Operation operation = await _operationRepository.CreateAsync(entity);

            var model = new OperationModel
            {
                Id = operation.Id,
                ExecutionTime = operation.ExecutionTime,
                ApplicationUserId = operation.ApplicationUserId
            };

            await _operationUpdateSender.SendOperation(model);

            OperationCreateResponse response = entity.ToDTO();

            return response;
        }

        public async Task<OperationSummaryResponse> GetFilteredOperationsAsync(int pageSize, int pageIndex)
        {
            OperationSummaryResponse response = await _operationRepository.GetFilteredOperationsAsync(pageSize, pageIndex);
            return response;
        }

        public async Task UpdateAsync(OperationModel operationModel)
        {
            Operation operation = await _operationRepository.GetByIdAsync(operationModel.Id);
            operation.ExecutionTime = operationModel.ExecutionTime;
            operation.Status = OperationStatus.Completed;
            await _operationRepository.UpdateAsync(operation);
        }
    }
}
