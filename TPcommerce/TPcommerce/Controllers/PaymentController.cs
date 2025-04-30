using System.IdentityModel.Tokens.Jwt;
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
        private readonly ProductRepository _productRepository;

        public PaymentController(IOptions<StripeSettings> stripeSettings, UserRepository userRepository, ShoppingCartRepository shoppingCartRepository, ProductRepository productRepository)
        {
            _stripeSettings = stripeSettings.Value;
            _userRepository = userRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _productRepository = productRepository;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("JWT");
            if (token == null)
            {
                TempData["message"] = "Veuillez vous connecter.";
                return RedirectToAction("Index", "User");
            }
            
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            var userId = jwt.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            var shoppingCartResponse = _shoppingCartRepository.GetShoppingCart(Convert.ToInt32(userId)).Result;
            
            if (!shoppingCartResponse.Success)
            {
                TempData["message"] = shoppingCartResponse.Message;
                return RedirectToAction("Index", "Home");
            }
            
            foreach (var shoppingCartItem in shoppingCartResponse.Data.ShoppingCartItems)
            {
                var product = await _productRepository.GetSingleProduct(shoppingCartItem.ProductId);
                shoppingCartItem.Product = product.Data;
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
        public async Task<IActionResult> Charge(string stripeToken)
        {
            var token = HttpContext.Session.GetString("JWT");
            if (token == null)
            {
                TempData["message"] = "Veuillez vous connecter.";
                return RedirectToAction("Index", "User");
            }
            
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            var userId = jwt.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            var user = _userRepository.ShowUserDetails(Convert.ToInt32(userId), token).Result;
            var cartResponse = await _shoppingCartRepository.GetShoppingCart(Convert.ToInt32(userId));
            if (!cartResponse.Success)
            {
                TempData["message"] = cartResponse.Message;
                return RedirectToAction("Index", "ShoppingCart");
            }
            
            foreach (var shoppingCartItem in cartResponse.Data.ShoppingCartItems)
            {
                var product = await _productRepository.GetSingleProduct(shoppingCartItem.ProductId);
                shoppingCartItem.Product = product.Data;
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
                            Quantity = i.Quantity,
                            Product = i.Product
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
            
                    return View("PaymentSuccess", bill);
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