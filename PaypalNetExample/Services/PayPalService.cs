using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;
using PaypalNetExample.Models;
namespace PaypalNetExample.Services
{
    public class PayPalService
    {
        private readonly IConfiguration _config;

        public PayPalService(IConfiguration config)
        {
            _config = config;
        }

        private PayPalEnvironment GetEnvironment()
        {
            var clientId = _config["PayPal:ClientId"];
            var secret = _config["PayPal:Secret"];
            var environment = _config["PayPal:Environment"];

            return environment == "live"
                ? new LiveEnvironment(clientId, secret)
                : new SandboxEnvironment(clientId, secret);
        }

        private PayPalHttpClient GetClient() => new PayPalHttpClient(GetEnvironment());

        public async Task<CreateOrderResponse> CreateOrderAsync(decimal amount, string currency = "MXN")
        {
            var request = new OrdersCreateRequest();
            request.Prefer("return=representation");

            request.RequestBody(new OrderRequest()
            {
                CheckoutPaymentIntent = "CAPTURE",
                PurchaseUnits = new List<PurchaseUnitRequest>
                {
                    new PurchaseUnitRequest
                    {
                        AmountWithBreakdown = new AmountWithBreakdown
                        {
                            CurrencyCode = currency,
                            Value = amount.ToString("F2")
                        }
                    }
                },
                ApplicationContext = new ApplicationContext
                {
                    ReturnUrl = "https://localhost:7016/paypal/success",
                    CancelUrl = "https://localhost:7016/paypal/cancel",
                    Locale = "es-MX"
                }
            });

            var client = GetClient();
            var response = await client.Execute(request);
            var result = response.Result<Order>();

            return new CreateOrderResponse
            {
                Id = result.Id,
            };
        }

        // Capturar orden (cobrar)
        public async Task<CaptureOrderResponse> CaptureOrderAsync(string orderId)
        {
            var request = new OrdersCaptureRequest(orderId);
            request.Prefer("return=representation");
            request.RequestBody(new OrderActionRequest());

            var client = GetClient();
            var response = await client.Execute(request);
            var result = response.Result<Order>();

            return new CaptureOrderResponse
            {
                Id = result.Id,
                Status = result.Status,
            };
        }
    }

}
