using EcWebapi.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcWebapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StoreController(StoreService storeService) : ControllerBase
    {
        private readonly StoreService _storeService = storeService;

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _storeService.GetAsync());
        }
    }
}