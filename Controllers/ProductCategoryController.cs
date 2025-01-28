using EcWebapi.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcWebapi.Controllers
{
    [Route("productCategories")]
    [ApiController]
    public class ProductCategoryController(ProductCategoryService productCategoryService) : ControllerBase
    {
        private readonly ProductCategoryService _productCategoryService = productCategoryService;

        [HttpGet]
        public async Task<IActionResult> GetListAsync()
        {
            return Ok(await _productCategoryService.GetProductCategoryAsync());
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            return Ok(await _productCategoryService.GetAsync(id));
        }
    }
}