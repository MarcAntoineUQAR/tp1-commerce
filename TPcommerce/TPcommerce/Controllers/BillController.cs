using Microsoft.AspNetCore.Mvc;

namespace TPcommerce.Controllers;

public class BillController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}