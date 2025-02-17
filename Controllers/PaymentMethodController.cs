using EcWebapi.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcWebapi.Controllers
{
    [Route("paymentMethods")]
    [ApiController]
    public class PaymentMethodController(PayementMethodService payementMethodService) : ControllerBase
    {
        private PayementMethodService _payementMethodService = payementMethodService;

        [HttpGet]
        public async Task<IActionResult> GetListAsync()
        {
            return Ok(await _payementMethodService.GetListAsync());
        }
    }
}