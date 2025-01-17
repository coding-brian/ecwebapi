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

        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto dto)
        {
            var result = await _memberService.Login(dto);

            if (!result.IsSuccess) return BadRequest(result);

            return Ok(result.Data);
        }
    }
}