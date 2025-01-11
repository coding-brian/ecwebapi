using EcWebapi.Dto;
using EcWebapi.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcWebapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController(MemberService memberService) : ControllerBase
    {
        private readonly MemberService _memberService = memberService;

        [HttpGet]
        public async Task<IActionResult> LoginAsync([FromQuery] LoginDto dto)
        {
            var token = await _memberService.Login(dto);

            if (token == null) return BadRequest();

            return Ok(token);
        }
    }
}