using EcWebapi.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcWebapi.Controllers
{
    [Route("news")]
    [ApiController]
    public class NewsController(NewsService newsService) : ControllerBase
    {
        private readonly NewsService _newsService = newsService;

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _newsService.GetAsync());
        }
    }
}