using Microsoft.AspNetCore.Mvc;

namespace EcWebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpGet]
        public IActionResult Login()
        {
            return Ok();
        }
    }
}