namespace TPcommerce.Models;

public class UserModel : TPcommerce.Models.User
{
    public ShoppingCart? ShoppingCart { get; set; }
}