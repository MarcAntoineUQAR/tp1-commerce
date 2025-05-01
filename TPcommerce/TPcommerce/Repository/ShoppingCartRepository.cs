using Microsoft.EntityFrameworkCore;
using TPcommerce.Models;

namespace TPcommerce.Repository;

public class ShoppingCartRepository
{
    private readonly UserRepository _userRepository;
    private readonly ProductRepository _productRepository;
    private TpcommerceContext context = new TpcommerceContext();
    private HttpClient client = new HttpClient();
    
    public ShoppingCartRepository(HttpClient httpClient, UserRepository userRepository, ProductRepository productRepository)
    {
        _userRepository = userRepository;
        _productRepository = productRepository;
        client = httpClient;
    }

    public async Task<GenericResponse<ShoppingCart>> GetShoppingCart(int userId)
    {
        var response = await client.GetAsync($"http://localhost:5213/Cart/user/{userId}");
        var shoppingcart = await response.Content.ReadFromJsonAsync<ShoppingCart>();
        return new GenericResponse<ShoppingCart>(shoppingcart, "success", true);
    }


    public GenericResponse<string> AddProductToShoppingCart(int shoppingcartid, int productId, int quantity)
    {
        // try
        // {
        //     var shoppingCartItem = new ShoppingCartItem()
        //     {
        //         ProductId = productId,
        //         Quantity = quantity,
        //         ShoppingCartId = shoppingcartid,
        //     };
        //     
        //     context.ShoppingCartItems.Add(shoppingCartItem);
        //     context.SaveChanges();
        //     return new GenericResponse<string>("Produit ajouté avec succès au panier!", true);
        // }
        // catch (Exception e)
        // {
        //     return new GenericResponse<string>("Erreur inattendue: " + e.Message, false);
        // }
        return null;
    }

    public GenericResponse<string> RemoveProductFromShoppingCart(int shoppingcartid, int productId)
    {
        // try
        // {
        //     var shoppingCartItem = context.ShoppingCartItems
        //         .FirstOrDefault(sci => sci.ProductId == productId && sci.ShoppingCartId == shoppingcartid);
        //
        //     if (shoppingCartItem == null)
        //     {
        //         return new GenericResponse<string>("Le produit n'existe pas dans le panier.", false);
        //     }
        //
        //     context.ShoppingCartItems.Remove(shoppingCartItem);
        //     context.SaveChanges();
        //
        //     return new GenericResponse<string>("Produit supprimé avec succès du panier!", true);
        // }
        // catch (Exception e)
        // {
        //     return new GenericResponse<string>("Erreur inattendue: " + e.Message, false);
        // }    
        return null;
    }

    public GenericResponse<string> ClearShoppingCart(int schoppingcartid)
    {
        // try
        // {
        //     var shoppiungcartitem = context.ShoppingCartItems.Where(si => si.ShoppingCartId == schoppingcartid);
        //     context.ShoppingCartItems.RemoveRange(shoppiungcartitem);
        //     context.SaveChanges();
        //     return new GenericResponse<string>("Suppression du panier faites avec succès!", true);
        // }
        // catch (Exception e)
        // {
        //     return new GenericResponse<string>("Erreur inattendue: " + e.Message, false);
        // }
        return null;
    }
}