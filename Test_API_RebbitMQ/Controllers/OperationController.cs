using Core.Entities;
using Core.Interfaces.Core.Services;
using Core.Models.Operations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test_API_RebbitMQ.Controllers
{
    public class OperationController : BaseApiController
    {
        protected readonly IOperationService _operationService;

        public OperationController(IOperationService operationService)
        {
            _operationService = operationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFilteredOperationsAsync(int pageSize, int pageIndex)
        {
            OperationSummaryResponse response = await _operationService.GetFilteredOperationsAsync(pageSize,pageIndex);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] OperationCreateRequest request)
        {
            Operation operation = await _operationService.CreateAsync(request);
            return Ok(operation);
        }


    }
}
