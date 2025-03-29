using TPcommerce.Models.DTO;

namespace TPcommerce.Models;

public class UserModel : User
{
    public int ShoppingCartId { get; set; }
    
    public ShoppingCart ShoppingCart { get; set; }
}