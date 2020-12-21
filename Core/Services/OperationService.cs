﻿using Core.Entities;
using Core.Enums;
using Core.Interfaces.Core.Services;
using Core.Interfaces.Infrastructure;
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
        public OperationService(IOperationRepository operationRepository)
        {
            _operationRepository = operationRepository;
        }
        public async Task<Operation> CreateAsync(OperationCreateRequest request)
        {
            var entity = new Operation
            {
                CreationDate = DateTime.Now,
                Name = request.Name,
                Status = OperationStatus.NotStarted,
                UserId = request.UserId
            };

            await _operationRepository.CreateAsync(entity);

            return entity;
        }

        public async Task<OperationSummaryResponse> GetFilteredOperationsAsync(int pageSize, int pageIndex)
        {
            OperationSummaryResponse response = await _operationRepository.GetFilteredOperationsAsync(pageSize, pageIndex);
            return response;
        }

        public Task<Operation> UpdateAsync(OperationUpdateRequest request)
        {
            throw new NotImplementedException();
        }
    }
}