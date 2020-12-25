using Core.Interfaces.Core.Services;
using Core.Models.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Test_API_RebbitMQ.Controllers
{
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
