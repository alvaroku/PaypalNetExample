using Microsoft.AspNetCore.Mvc;
using PaypalNetExample.Models;
using PaypalNetExample.Services;

namespace PaypalNetExample.Controllers
{
    public class PaymentController : Controller
    {
        private readonly PayPalService _paypalService;

        public PaymentController(PayPalService paypalService)
        {
            _paypalService = paypalService;
        }

        public async Task<IActionResult> Create(decimal amount)
        {
            CreateOrderResponse orderJson = await _paypalService.CreateOrderAsync(amount);
            return Ok(orderJson);
        }

        [HttpGet("paypal/success")]
        public async Task<IActionResult> Success(string token)
        {
            CaptureOrderResponse result = await _paypalService.CaptureOrderAsync(token);
            return Ok(result);
        }

        [HttpGet("paypal/cancel")]
        public IActionResult Cancel()
        {
            return Content("Pago cancelado por el usuario");
        }
    }

}
