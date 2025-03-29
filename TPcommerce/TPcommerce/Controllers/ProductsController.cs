using Microsoft.AspNetCore.Mvc;
using TPcommerce.Models;
using TPcommerce.Models.DTO;
using TPcommerce.Repository;

namespace TPcommerce.Controllers;

public class ProductsController : Controller
{
    private readonly ProductRepository _productRepository;
    private readonly UserRepository _userRepository;

    public ProductsController(ProductRepository productRepository, UserRepository userRepository)
    {
        _productRepository = productRepository;
        _userRepository = userRepository;
    }

    [HttpGet("products")]
    public IActionResult Index(string? searchTerm)
    {
        var user = GetUser();

        var result = string.IsNullOrEmpty(searchTerm)
            ? _productRepository.GetProducts()
            : _productRepository.GetProductsBySearchTerm(searchTerm);

        if (result.Success)
        {
            TempData["Role"] = user.Role;

            if (!string.IsNullOrEmpty(searchTerm) && result.Data.Count == 0)
            {
                TempData["Message"] = "No products found";
            }

            return View("../products/products", result.Data);
        }
        else
        {
            TempData["Message"] = result.Message;
            return RedirectToAction("Index", "Home");
        }
    }

    public IActionResult Details(int id)
    {
        var result = _productRepository.GetSingleProduct(id);
        if (result.Success && result.Data != null)
        {
            return View("../products/specificproduct", result.Data);
        }
        else
        {
            TempData["Message"] = result.Message;
            return RedirectToAction("Index");
        }
    }

    [HttpGet("products/add")]
    public IActionResult GetAddProduct()
    {
        var user = GetUser();
        if (user.Role != "seller")
        {
            TempData["message"] = "Tu n'es pas autorisé à être ici!";
            return RedirectToAction("Index", "Home");
        }

        return View("../products/addproduct");
    }

    [HttpPost("products/add")]
    public IActionResult AddProduct([FromForm] Product product)
    {
        var result = _productRepository.CreateProduct(product);
        TempData["message"] = result.Message;
        return RedirectToAction("Index");
    }

    private User GetUser()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        var user = _userRepository.ShowUserDetails(userId.Value);
        return user;
    }
}