using Microsoft.AspNetCore.Mvc;
using Crypto.Application.Auth;
namespace Crypto.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _auth;

        public AuthController(AuthService auth)
        {
            _auth = auth;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            await _auth.Register(request);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var token = await _auth.Login(request);

            if (token == null)
                return Unauthorized();

            return Ok(token);
        }
    }
}
