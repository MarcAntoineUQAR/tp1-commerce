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
    private User GetUser()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        var user = _userRepository.ShowUserDetails(userId.Value);
        return user;
    }
    
}