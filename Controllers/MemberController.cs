using EcWebapi.Dto;
using EcWebapi.Dto.Member;
using EcWebapi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcWebapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MemberController(MemberService memberService) : ControllerBase
    {
        private readonly MemberService _memberService = memberService;

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAsync()

        {
            return Ok(await _memberService.GetAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateMemberDto dto)

        {
            return Ok(await _memberService.CreateAsync(dto));
        }

        [HttpPut]
        public async Task<IActionResult> PostAsync([FromBody] UpdateMemberDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(await _memberService.UpdateAsync(dto));
        }

        [HttpPost("captcha")]
        public async Task<IActionResult> CreateCaptchaAsync([FromBody] CreateCaptchaDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(await _memberService.CreateMemberCaptchaAsync(dto));
        }
    }
}