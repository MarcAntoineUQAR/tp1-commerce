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
        // var user = GetUser();
        //
        // var result = string.IsNullOrEmpty(searchTerm)
        //     ? _productRepository.GetProducts()
        //     : _productRepository.GetProductsBySearchTerm(searchTerm);
        //
        // if (result.Success)
        // {
        //     TempData["Role"] = user.Role;
        //
        //     if (user.Role == "seller")
        //     {
        //         var context = new TpcommerceContext();
        //         var sellerProducts = context.Products.Where(p => p.SellerId == user.Id).ToList();
        //         ViewBag.SellerProducts = sellerProducts;
        //     }
        //
        //     if (!string.IsNullOrEmpty(searchTerm) && result.Data.Count == 0)
        //     {
        //         TempData["Message"] = "No products found";
        //     }
        //
        //     return View("../products/products", result.Data);
        // }
        // else
        // {
        //     TempData["Message"] = result.Message;
        //     return RedirectToAction("Index", "Home");
        // }
        return new EmptyResult();
    }


    public IActionResult Details(int id)
    {
        // var result = _productRepository.GetSingleProduct(id);
        // if (result.Success && result.Data != null)
        // {
        //     return View("../products/specificproduct", result.Data);
        // }
        // else
        // {
        //     TempData["Message"] = result.Message;
        //     return RedirectToAction("Index");
        // }
        return new EmptyResult();
    }

    [HttpGet("products/add")]
    public IActionResult GetAddProduct()
    {
        // var user = GetUser();
        // if (user.Role != "seller")
        // {
        //     TempData["message"] = "Tu n'es pas autorisé à être ici!";
        //     return RedirectToAction("Index", "Home");
        // }
        //
        // var context = new TpcommerceContext();
        // var categories = context.Products
        //     .Select(p => p.Category)
        //     .Distinct()
        //     .OrderBy(c => c)
        //     .ToList();
        //
        // ViewBag.Categories = categories;
        //
        // return View("../products/addproduct");
        return new EmptyResult();
    }


    [HttpPost("products/add")]
    public IActionResult AddProduct([FromForm] Product product)
    {
        // var user = GetUser();
        // if (user.Role != "seller")
        // {
        //     TempData["message"] = "Tu n'es pas autorisé à faire ça!";
        //     return RedirectToAction("Index", "Home");
        // }
        //
        // product.SellerId = user.Id;
        // var result = _productRepository.CreateProduct(product);
        // TempData["message"] = result.Message;
        // return RedirectToAction("Index");
        return new EmptyResult();
    }


    private User GetUser()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        var user = _userRepository.ShowUserDetails(userId.Value);
        return user;
    }
}