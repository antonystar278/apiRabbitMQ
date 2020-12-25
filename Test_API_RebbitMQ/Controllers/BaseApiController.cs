using Microsoft.AspNetCore.Mvc;
using Test_API_RebbitMQ.Filters;

namespace Test_API_RebbitMQ.Controllers
{
    [TypeFilter(typeof(CustomExceptionFilter))]
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {

    }
}
