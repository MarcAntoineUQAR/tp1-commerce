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

}