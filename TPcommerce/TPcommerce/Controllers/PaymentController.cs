using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Stripe;
using TPcommerce.Models;
using TPcommerce.Repository;

namespace TPcommerce.Controllers
{
    public class PaymentController : Controller
    {
        private readonly StripeSettings _stripeSettings;
        private readonly UserRepository _userRepository;
        private readonly ShoppingCartRepository _shoppingCartRepository;

        public PaymentController(IOptions<StripeSettings> stripeSettings, UserRepository userRepository, ShoppingCartRepository shoppingCartRepository)
        {
            _stripeSettings = stripeSettings.Value;
            _userRepository = userRepository;
            _shoppingCartRepository = shoppingCartRepository;
        }


        [HttpGet]
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                TempData["message"] = "Veuillez vous connecter.";
                return RedirectToAction("Login", "User");
            }

            var shoppingCartResponse = _shoppingCartRepository.GetShoppingCart(userId.Value);
            if (!shoppingCartResponse.Success)
            {
                TempData["message"] = shoppingCartResponse.Message;
                return RedirectToAction("Index", "Home");
            }

            var shoppingCart = shoppingCartResponse.Data;
            shoppingCart.TotalPrice = shoppingCart.ShoppingCartItems
                .Select(item => item.Product.Price * item.Quantity)
                .Sum();

            ViewBag.StripePublishableKey = _stripeSettings.PublishableKey;
            return View("../Payment/Payment", shoppingCart);
        }


        [HttpGet]
        public IActionResult PaymentSuccess()
        {
            return View();
        }

        [HttpGet]
        public IActionResult PaymentFailure()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout()
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Charge(string stripeToken)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "User");

            var user = _userRepository.ShowUserDetails(userId.Value);
            var cartResponse = _shoppingCartRepository.GetShoppingCart(user.Id);
            if (!cartResponse.Success)
            {
                TempData["message"] = cartResponse.Message;
                return RedirectToAction("Index", "ShoppingCart");
            }

            var cart = cartResponse.Data;
            cart.TotalPrice = cart.ShoppingCartItems.Sum(i => i.Quantity * i.Product.Price);

            var chargeOptions = new ChargeCreateOptions
            {
                Amount = (long)(cart.TotalPrice * 100),
                Currency = "cad",
                Description = $"Achat par {user.Username}",
                Source = stripeToken,
                ReceiptEmail = "client@example.com",
                Shipping = new ChargeShippingOptions
                {
                    Name = user.Username,
                    Address = new AddressOptions
                    {
                        Line1 = "123 Rue Exemple",
                        City = "Lévis",
                        State = "QC",
                        PostalCode = "G6V0A6",
                        Country = "CA"
                    }
                }
            };

            var chargeService = new ChargeService();
            try
            {
                var charge = chargeService.Create(chargeOptions);
                if (charge.Status == "succeeded")
                {
                    var bill = new Bill
                    {
                        Products = cart.ShoppingCartItems.Select(i => new BillItem
                        {
                            ProductId = i.ProductId,
                            Quantity = i.Quantity
                        }).ToList(),
                        TotalPrice = (int)cart.TotalPrice,
                        OwnerId = user.Id.ToString(),
                        PaymentInfos = new PaymentInfos
                        {
                            FullName = user.Username,
                            CardType = "Stripe",
                            CardNumber = "**** **** **** 4242",
                            ExpirationDate = "04/29",
                            CVV = "***"
                        }
                    };


                    using var context = new TpcommerceContext();
                    context.Bills.Add(bill);
                    context.SaveChanges();
                    _shoppingCartRepository.ClearShoppingCart(user.ShoppingCart.Id);


                    var fullBill = context.Bills
                    .Include(b => b.Products)
                        .ThenInclude(p => p.Product)
                    .Include(b => b.PaymentInfos)
                    .FirstOrDefault(b => b.Id == bill.Id);

                    return View("PaymentSuccess", fullBill);
                }
                else
                {
                    return RedirectToAction("PaymentFailure");
                }
            }
            catch (StripeException)
            {
                return RedirectToAction("PaymentFailure");
            }
        }

    }
}