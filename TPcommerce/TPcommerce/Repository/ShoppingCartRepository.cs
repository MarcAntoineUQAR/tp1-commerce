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
            var result = context.ShoppingCarts.Find(userId);
            if (result == null)
            {
                return new GenericResponse<ShoppingCart>("Le panier existe pas", false);
            }
            return new GenericResponse<ShoppingCart>(result,"réussi", true);
        }
        catch (Exception e)
        {
            return new GenericResponse<ShoppingCart>("Erreur inattendu: " + e.Message, false);
        }
    }
}