using EcWebapi.Dto;
using EcWebapi.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcWebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController(MemberService service) : ControllerBase
    {
        private readonly MemberService _service = service;

        [HttpPost]
        public async Task<IActionResult> RegisterAsync([FromBody] MemberDto dto)

        {
            return Ok(await _service.RegisterAsync(dto));
        }
    }
}