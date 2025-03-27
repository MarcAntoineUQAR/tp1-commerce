using Microsoft.AspNetCore.Mvc;

namespace TPcommerce.Controllers;

public class ProductsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View("../Products");
    }
}