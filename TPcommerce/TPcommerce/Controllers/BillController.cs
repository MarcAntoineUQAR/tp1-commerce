using Microsoft.AspNetCore.Mvc;

namespace TPcommerce.Controllers;

public class BillController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}