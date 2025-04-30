using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TPcommerce.Repository;

namespace TPcommerce.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController()
    {
    }

    public IActionResult Index()
    {
        return View();
    }

    // Sert a peupler la bd pusique le modelOnconfirguring marche pas
    [HttpGet]
    public IActionResult FirstConnection()
    {
        // _repository.PopulateDbContext();
        return RedirectToAction("Index", "Login");
    }
}
