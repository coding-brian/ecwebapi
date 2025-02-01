using EcWebapi.Dto.Product;
using EcWebapi.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcWebapi.Controllers
{
    [Route("products")]
    [ApiController]
    public class ProductController(ProductService productService) : ControllerBase
    {
        private readonly ProductService _productService = productService;

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] QueryProductDto dto)
        {
            return Ok(await _productService.GetAsync(dto));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            return Ok(await _productService.GetAsync(id));
        }
    }
}