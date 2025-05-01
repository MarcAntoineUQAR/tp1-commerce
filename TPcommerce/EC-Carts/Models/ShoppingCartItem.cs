namespace EC_Carts.Models;

public class ShoppingCartItem
{
    public int Id { get; set; }
    public int ShoppingCartId { get; set; }
    public ShoppingCart ShoppingCart { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}