namespace TPcommerce.Models;

public class UserModel : TPcommerce.Models.User
{
    public int ShoppingCartId { get; set; }
    
    public ShoppingCart ShoppingCart { get; set; }
}