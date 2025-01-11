using EcWebapi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcWebapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class MemberController(MemberService memberService) : ControllerBase
    {
        private readonly MemberService _memberService = memberService;

        [HttpGet]
        public async Task<IActionResult> GetAsync()

        {
            return Ok(await _memberService.GetAsync());
        }
    }
}