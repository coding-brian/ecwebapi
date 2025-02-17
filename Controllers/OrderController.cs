using EcWebapi.Dto.Order;
using EcWebapi.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcWebapi.Controllers
{
    [Route("orders")]
    [ApiController]
    public class OrderController(OrderService orderService) : ControllerBase
    {
        private readonly OrderService _orderService = orderService;

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateOrderDto dto)
        {
            if (ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _orderService.CreateAsync(dto));
        }
    }
}