using Microsoft.EntityFrameworkCore;
using TPcommerce.Models;

namespace TPcommerce.Repository;

public class ShoppingCartRepository
{
    private readonly UserRepository _userRepository;
    private readonly ProductRepository _productRepository;
    private TpcommerceContext context = new TpcommerceContext();
    
    public ShoppingCartRepository(UserRepository userRepository, ProductRepository productRepository)
    {
        _userRepository = userRepository;
        _productRepository = productRepository;
    }

    public GenericResponse<ShoppingCart> GetShoppingCart(int userId)
    {
        try
        {
            var result = context.ShoppingCarts
                .Include(s => s.Owner)
                .Include(s => s.ShoppingCartItems)
                .ThenInclude(i => i.Product)
                .FirstOrDefault(s => s.OwnerId == userId);

            if (result == null)
            {
                return new GenericResponse<ShoppingCart>("Le panier n'existe pas pour cet utilisateur", false);
            }

            return new GenericResponse<ShoppingCart>(result, "Réussi", true);
        }
        catch (Exception e)
        {
            return new GenericResponse<ShoppingCart>("Erreur inattendue: " + e.Message, false);
        }
    }


    public GenericResponse<string> AddProductToShoppingCart(int shoppingcartid, int productId, int quantity)
    {
        try
        {
            var shoppingCartItem = new ShoppingCartItem()
            {
                ProductId = productId,
                Quantity = quantity,
                ShoppingCartId = shoppingcartid,
            };
            
            context.ShoppingCartItems.Add(shoppingCartItem);
            context.SaveChanges();
            return new GenericResponse<string>("Produit ajouté avec succès au panier!", true);
        }
        catch (Exception e)
        {
            return new GenericResponse<string>("Erreur inattendue: " + e.Message, false);
        }
    }

    public GenericResponse<string> RemoveProductFromShoppingCart(int shoppingcartid, int productId)
    {
        try
        {
            var shoppingCartItem = context.ShoppingCartItems
                .FirstOrDefault(sci => sci.ProductId == productId && sci.ShoppingCartId == shoppingcartid);

            if (shoppingCartItem == null)
            {
                return new GenericResponse<string>("Le produit n'existe pas dans le panier.", false);
            }

            context.ShoppingCartItems.Remove(shoppingCartItem);
            context.SaveChanges();

            return new GenericResponse<string>("Produit supprimé avec succès du panier!", true);
        }
        catch (Exception e)
        {
            return new GenericResponse<string>("Erreur inattendue: " + e.Message, false);
        }
    }

    public GenericResponse<string> ClearShoppingCart(int schoppingcartid)
    {
        try
        {
            var shoppiungcartitem = context.ShoppingCartItems.Where(si => si.ShoppingCartId == schoppingcartid);
            context.ShoppingCartItems.RemoveRange(shoppiungcartitem);
            context.SaveChanges();
            return new GenericResponse<string>("Suppression du panier faites avec succès!", true);
        }
        catch (Exception e)
        {
            return new GenericResponse<string>("Erreur inattendue: " + e.Message, false);
        }
    }
}