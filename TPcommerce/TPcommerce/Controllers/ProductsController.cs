using Microsoft.AspNetCore.Mvc;
using TPcommerce.Repository;

namespace TPcommerce.Controllers;

public class ProductsController : Controller
{
    private ProductRepository _productRepository;
    public ProductsController(ProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    [HttpGet("products")]
    public IActionResult Index()
    {
        var result = _productRepository.GetProducts();
        if (result.Success)
        {
            return View("../products",result.Data);
        }
        else
        {
            TempData["Message"] = result.Message;
            return RedirectToAction("Index", "Home");
        }
    }
}