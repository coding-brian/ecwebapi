using EcWebapi.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcWebapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TokenController(TokenService tokenService) : ControllerBase
    {
        private readonly TokenService _tokenService = tokenService;

        [HttpPost("refresh")]
        public async Task<IActionResult> CreateRefreshToken(string refreshToken)
        {
            return Ok(await _tokenService.GetRefreshToken(refreshToken));
        }
    }
}