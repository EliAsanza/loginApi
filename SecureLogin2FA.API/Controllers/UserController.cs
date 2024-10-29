using Microsoft.AspNetCore.Mvc;
using SecureLogin2FA.Domain.Interfaces.Services;
using SecureLogin2FA.Domain.Models.Users;
using System.Threading.Tasks;

namespace SecureLogin2FA.API.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userService.CreateUserAsync(userModel);

            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return StatusCode(500, "An error occurred while creating the user.");
            }
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }
    }
}
