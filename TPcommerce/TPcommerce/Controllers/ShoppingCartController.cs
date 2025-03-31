using Microsoft.AspNetCore.Mvc;
using TPcommerce.Models;
using TPcommerce.Models.DTO;
using TPcommerce.Repository;

namespace TPcommerce.Controllers;

public class ShoppingCartController : Controller
{
    private readonly UserRepository _userRepository;
    private readonly ShoppingCartRepository _shoppingCartRepository;

    public ShoppingCartController(UserRepository userRepository, ShoppingCartRepository shoppingCartRepository)
    {
        _userRepository = userRepository;
        _shoppingCartRepository = shoppingCartRepository;
    }

    [HttpGet("shoppingcart")]
    public IActionResult Index()
    {
        var user = GetUser();
        var shoppingCart = _shoppingCartRepository.GetShoppingCart(user.Id);
        decimal totalPrice = shoppingCart.Data.ShoppingCartItems
            .Select(p => p.Product.Price * p.Quantity).Sum();
        shoppingCart.Data.TotalPrice = totalPrice;
        
        if (shoppingCart.Success)
        {
            return View("../shoppingcart",shoppingCart.Data);
        }
        else
        {
            TempData["message"] = shoppingCart.Message;
            return RedirectToAction("Index", "Home");
        }
    }

    [HttpPost("shoppingcart/add")]
    public IActionResult AddProductToShoppingCart(int productId, int quantity)
    {
        var user = GetUser();
        if (user == null)
        {
            TempData["message"] = "Veuillez vous connecter avant d'ajouter au panier.";
            return RedirectToAction("Login", "Login");
        }
        var results = _shoppingCartRepository.AddProductToShoppingCart(user.ShoppingCart.Id, productId, quantity);
        TempData["message"] = results.Message;
        return RedirectToAction("Index", "Products");
    }

    [HttpPost("shoppingcart/remove")]
    public IActionResult RemoveProductFromShoppingCart(int productId)
    {
        var user = GetUser();
        var results = _shoppingCartRepository.RemoveProductFromShoppingCart(user.ShoppingCart.Id, productId);
        TempData["message"] = results.Message;
        return RedirectToAction("Index");
    }

    [HttpPost("shoppingcart/clear")]
    public IActionResult ClearShoppingCart()
    {
        var user = GetUser();
        var results = _shoppingCartRepository.ClearShoppingCart(user.ShoppingCart.Id);
        TempData["message"] = results.Message;
        return RedirectToAction("Index");
    }
    
    private User GetUser()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        var user = _userRepository.ShowUserDetails(userId.Value);
        return user;
    }
}