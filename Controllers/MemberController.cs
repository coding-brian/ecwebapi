using EcWebapi.Dto;
using EcWebapi.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcWebapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MemberController(MemberService memberService) : ControllerBase
    {
        private readonly MemberService _memberService = memberService;

        [HttpGet]
        public async Task<IActionResult> GetAsync()

        {
            return Ok(await _memberService.GetAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] MemberDto dto)

        {
            return Ok(await _memberService.CreateAsync(dto));
        }

        [HttpPost("captcha")]
        public async Task<IActionResult> CreateCaptchaAsync([FromBody] CreateCaptchaDto dto)
        {
            if (!ModelState.IsValid) return BadRequest();

            return Ok(await _memberService.CreateMemberCaptchaAsync(dto));
        }
    }
}