using Ardalis.ApiEndpoints;
using Core.Entities;
using Core.Helpers;
using Core.Interfaces.Core.Services;
using Core.Models.Operations;
using Core.Models.Operations.Create;
using Core.Models.Operations.ListPaged;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using Test_API_RebbitMQ.Filters;

namespace Test_API_RebbitMQ.Controllers
{
    [Authorize]
    [TypeFilter(typeof(CustomExceptionFilter))]
    public class OperationsController: BaseApiController
    {
        protected readonly IOperationService _operationService;

        public OperationsController(IOperationService operationService)
        {
            _operationService = operationService;
        }


        [HttpGet("api/operation")]
        public async Task<IActionResult> GetFilteredOperationsAsync(int pageSize = 15, int pageIndex = 1)
        {
            OperationSummaryResponse response = await _operationService.GetFilteredOperationsAsync(pageSize,pageIndex);
            return Ok(response);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] OperationCreateRequest request)
        {
            var operation = await _operationService.SendCreateAsync(request);
            return Ok(operation);
        }
    }
}
