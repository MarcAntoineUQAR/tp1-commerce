using Microsoft.AspNetCore.Mvc;
using TPcommerce.Repository;
using Microsoft.EntityFrameworkCore;

namespace TPcommerce.Controllers
{
    public class SalesController : Controller
    {
        private readonly UserRepository _userRepository;
        private readonly TpcommerceContext _context;

        public SalesController(UserRepository userRepository, TpcommerceContext context)
        {
            _userRepository = userRepository;
            _context = context;
        }

        [HttpGet("sales")]
        public IActionResult Index()
        {
            // var userId = HttpContext.Session.GetInt32("UserId");
            // if (userId == null)
            // {
                // TempData["message"] = "Connecte-toi pour voir tes ventes.";
                // return RedirectToAction("Login", "Login");
            // }

            // var user = _userRepository.ShowUserDetails(userId.Value);
            // if (user.Role != "seller")
            // {
                // TempData["message"] = "Seuls les vendeurs peuvent voir les ventes.";
                // return RedirectToAction("Index", "Home");
            // }

            // var sales = _context.BillItems
            //     .Where(bi => bi.Product.SellerId == user.Id)
            //     .Include(bi => bi.Product)
            //     .GroupBy(bi => bi.Product.Title)
            //     .Select(g => new
            //     {
            //         ProductName = g.Key,
            //         QuantitySold = g.Sum(x => x.Quantity),
            //         TotalRevenue = g.Sum(x => x.Quantity * x.Product.Price)
            //     }).ToList();

            // return View("~/Views/Sales.cshtml", sales);
            return null;
        }
    }

}
