using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TPcommerce.Repository;

namespace TPcommerce.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly BaseRepository _repository;

    public HomeController(ILogger<HomeController> logger, BaseRepository repository)
    {
        _repository = repository;
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    // Sert a peupler la bd pusique le modelOnconfirguring marche pas
    [HttpGet]
    public IActionResult FirstConnection()
    {
        _repository.PopulateDbContext();
        return RedirectToAction("Login", "Login");
    }
}
