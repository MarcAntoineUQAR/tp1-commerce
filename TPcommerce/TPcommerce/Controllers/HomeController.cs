using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TPcommerce.Models;
using TPcommerce.Repository;

namespace TPcommerce.Controllers;

public class HomeController : Controller
{
    private readonly ProductRepository _productRepository;

    public HomeController(ProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public IActionResult Index()
    {
        return View();
    }

    // Sert a peupler la bd pusique le modelOnconfirguring marche pas
    [HttpGet]
    public async Task<IActionResult> FirstConnection()
    {
        var hasProducts = await _productRepository.HasExistingProducts();
        if (!hasProducts)
        {
            await _productRepository.PopulateDbContext();
        }

        return RedirectToAction("Index", "Login");
    }

}
