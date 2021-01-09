using Ardalis.ApiEndpoints;
using Core.Helpers;
using Core.Interfaces.Core.Services;
using Core.Models.Operations.ListPaged;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Test_API_RebbitMQ.Filters;

namespace Test_API_RebbitMQ.Controllers
{
    [Authorize]
    [TypeFilter(typeof(CustomExceptionFilter))]
    public class ListPaged : BaseAsyncEndpoint<ListPagedOperationsRequest, ListPagedOperationsResponse>
    {
        protected readonly IOperationService _operationService;

        public ListPaged(IOperationService operationService)
        {
            _operationService = operationService;
        }
        [HttpGet("api/operations")]
        public async override Task<ActionResult<ListPagedOperationsResponse>> HandleAsync([FromQuery]ListPagedOperationsRequest request, CancellationToken cancellationToken = default)
        {
            if(request.PageSize != 15)
            {
                request.PageSize = 15;
            }

            ListPagedOperationsResponse response = await _operationService.GetFilteredDataAsync(request, cancellationToken);
            return Ok(response);
        }
    }
}
