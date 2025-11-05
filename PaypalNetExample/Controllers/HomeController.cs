using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PaypalNetExample.Models;

namespace PaypalNetExample.Controllers
{
    public class HomeController(ILogger<HomeController> _logger,IConfiguration configuration) : Controller
    {
 
        public IActionResult Index()
        {
            PaypalClientData data = new PaypalClientData
            {
                ClientId = configuration["PayPal:ClientId"],
                Currency = configuration["PayPal:Currency"],
                Locale = configuration["Paypal:Locale"],
                ClientUrl = configuration["Paypal:ClientUrl"]
            };
            return View(data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
