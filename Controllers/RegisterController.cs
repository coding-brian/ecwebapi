using EcWebapi.Dto;
using EcWebapi.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcWebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        public RegisterController(RegisterService service)
        {
            _service = service;
        }

        private readonly RegisterService _service;

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(MemberDto dto)

        {
            return Ok(await _service.RegisterAsync(dto));
        }
    }
}