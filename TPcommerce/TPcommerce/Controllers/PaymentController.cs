using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using TPcommerce.Models;

namespace TPcommerce.Controllers
{
    public class PaymentController : Controller
    {
        private readonly StripeSettings _stripeSettings;
        public PaymentController(IOptions<StripeSettings> stripeSettings)
        {
            _stripeSettings = stripeSettings.Value;
        }

        [HttpGet]
        public IActionResult Payment()
        {
            ViewBag.Title = "Page de paiement";
            ViewBag.StripePublishableKey = _stripeSettings.PublishableKey;
            return View();
        }

        [HttpPost]
        public IActionResult Charge(string stripeToken)
        {
            var chargeOptions = new ChargeCreateOptions
            {
                Amount = 5000,
                Currency = "cad",
                Description = "3 quintillion Robux",
                Source = stripeToken,

                ReceiptEmail = "jean.laronde@gmail.com",

                Shipping = new ChargeShippingOptions
                {
                    Name = "Jean Laronde",
                    Address = new AddressOptions
                    {
                        Line1 = "2233 Rue du Potier",
                        City = "Lévis",
                        State = "QC",
                        PostalCode = "G6V 0A6",
                        Country = "Canada"
                    }
                },
                Metadata = new Dictionary<string, string>
                {
                    { "customer_name", "Jean Laronde" },
                    { "customer_email", "jean.laronde@gmail.com" },
                    { "customer_address", "2233 Rue du Potier" },
                    { "customer_city", "Lévis" },
                    { "customer_state", "QC" },
                    { "customer_postal_code", "G6V 0A6" }
                }
            };
            var chargeService = new ChargeService();
            Charge charge = chargeService.Create(chargeOptions);

            if (charge.Status == "succeeded") return RedirectToAction("Success");
            else return RedirectToAction("Failure");
        }
    }
}
