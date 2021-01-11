using Core.Interfaces.Core.Services;
using Core.Models.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Test_API_RebbitMQ.Filters;

namespace Test_API_RebbitMQ.Controllers
{
    [TypeFilter(typeof(CustomExceptionFilter))]
    [Route("api/[controller]")]
    public class UserController : BaseApiController
    {
        private readonly IAccountService _accountService;
        public UserController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest model)
        {
            var response = await _accountService.AuthenticateAsync(model);

            return Ok(response);
        }
    }
}
