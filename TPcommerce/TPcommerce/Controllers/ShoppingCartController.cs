using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using TPcommerce.Models;
using TPcommerce.Models.DTO;
using TPcommerce.Repository;

namespace TPcommerce.Controllers;

public class ShoppingCartController : Controller
{
    private readonly UserRepository _userRepository;
    private readonly ShoppingCartRepository _shoppingCartRepository;
    private readonly ProductRepository _productRepository;

    public ShoppingCartController(UserRepository userRepository, ShoppingCartRepository shoppingCartRepository, ProductRepository productRepository)
    {
        _userRepository = userRepository;
        _shoppingCartRepository = shoppingCartRepository;
        _productRepository = productRepository;
    }

    [HttpGet("shoppingcart")]
    public async Task<IActionResult> Index()
    {
        var token = HttpContext.Session.GetString("JWT");
        if (token == null)
        {
            TempData["message"] = "Veuillez vous connecter.";
            return RedirectToAction("Index", "User");
        }
            
        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token);
        var userId = jwt.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
        var shoppingCartResponse = _shoppingCartRepository.GetShoppingCart(Convert.ToInt32(userId)).Result;
        
        if (shoppingCartResponse == null)
        {
            TempData["message"] = "Aucun panier trouv� pour l'utilisateur.";
            return RedirectToAction("Index", "Home");
        }
        
        if (shoppingCartResponse.Success && shoppingCartResponse.Data == null)
        {
            TempData["message"] = shoppingCartResponse.Message;
            return RedirectToAction("Index", "Home");
        }


        foreach (var shoppingCartItem in shoppingCartResponse.Data.ShoppingCartItems)
        {
            var product = await _productRepository.GetSingleProduct(shoppingCartItem.ProductId);
            shoppingCartItem.Product = product.Data;
        }
        
        
        decimal totalPrice = shoppingCartResponse.Data.ShoppingCartItems
            .Select(p => p.Product.Price * p.Quantity).Sum();
        shoppingCartResponse.Data.TotalPrice = totalPrice;
        
        return View("../shoppingcart", shoppingCartResponse.Data);
    }


    [HttpPost("shoppingcart/add")]
    public IActionResult AddProductToShoppingCart(int productId, int quantity)
    {
        // var user = GetUser();
        // if (user == null)
        // {
        //     TempData["message"] = "Veuillez vous connecter avant d'ajouter au panier.";
        //     return RedirectToAction("Login", "Login");
        // }
        //
        // if (user.Role == "seller")
        // {
        //     TempData["message"] = "Un vendeur ne peut pas acheter de produits.";
        //     return RedirectToAction("Index", "Products");
        // }
        //
        // if (user.ShoppingCart == null)
        // {
        //     TempData["message"] = "Aucun panier n'a �t� trouv� pour cet utilisateur.";
        //     return RedirectToAction("Index", "Home");
        // }
        //
        // var results = _shoppingCartRepository.AddProductToShoppingCart(user.ShoppingCart.Id, productId, quantity);
        // TempData["message"] = results.Message;
        // return RedirectToAction("Index", "Products");
        return new EmptyResult();
    }



    [HttpPost("shoppingcart/remove")]
    public IActionResult RemoveProductFromShoppingCart(int productId)
    {
        // var user = GetUser();
        // var results = _shoppingCartRepository.RemoveProductFromShoppingCart(user.ShoppingCart.Id, productId);
        // TempData["message"] = results.Message;
        // return RedirectToAction("Index");
        return new EmptyResult();
    }

    [HttpPost("shoppingcart/clear")]
    public IActionResult ClearShoppingCart()
    {
        // var user = GetUser();
        // var results = _shoppingCartRepository.ClearShoppingCart(user.ShoppingCart.Id);
        // TempData["message"] = results.Message;
        // return RedirectToAction("Index");
        return new EmptyResult();
    }
}