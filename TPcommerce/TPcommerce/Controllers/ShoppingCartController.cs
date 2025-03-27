using Microsoft.AspNetCore.Mvc;

namespace TPcommerce.Controllers;

public class ShoppingCartController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View("../ShoppingCart");
    }
}