using Microsoft.AspNetCore.Mvc;

namespace TPcommerce.Controllers;

public class PaymentController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}