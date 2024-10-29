using Microsoft.AspNetCore.Mvc;
using SecureLogin2FA.Domain.Interfaces.Services;
using SecureLogin2FA.Domain.Models.Users;
using System.Threading.Tasks;

namespace SecureLogin2FA.API.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost()]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var loginResult = await _loginService.LoginAsync(loginModel);

            if (loginResult.IsSuccessful)
            {
                return Ok(loginResult);
            }
            else
            {
                return Unauthorized(new { message = loginResult.Message });
            }
        }

        [HttpPost("verify-2fa")]
        public async Task<IActionResult> Verify2FA([FromBody] Verify2FAModel model)
        {
            var result = await _loginService.Verify2FATokenAsync(model.Email, model.Token);
            if (result)
            {
                return Ok(result);
            }
            return Unauthorized("Invalid or expired 2FA token.");
        }
    }
}