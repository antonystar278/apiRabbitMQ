using Ardalis.ApiEndpoints;
using Core.Helpers;
using Core.Interfaces.Core.Services;
using Core.Models.Operations.Create;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using Test_API_RebbitMQ.Filters;

namespace Test_API_RebbitMQ.Controllers.OperationEndpoints
{
    [Authorize]
    [TypeFilter(typeof(CustomExceptionFilter))]
    public class Create : BaseAsyncEndpoint<OperationCreateRequest, OperationCreateResponse>
    {
        protected readonly IOperationService _operationService;

        public Create(IOperationService operationService)
        {
            _operationService = operationService;
        }

        [HttpPost("api/operations")]
        public override async Task<ActionResult<OperationCreateResponse>> HandleAsync([FromBody] OperationCreateRequest request, CancellationToken cancellationToken = default)
        {
            var operation = await _operationService.CreateAsync(request);
            return Ok(operation);
        }
    }
}
