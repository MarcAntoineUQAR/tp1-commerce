using Microsoft.AspNetCore.Mvc;

namespace TPcommerce.Controllers;

public class TransactionHistoryController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View("../TransactionHistory");
    }
}