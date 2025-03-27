namespace TPcommerce.Models;

public class ShoppingCart
{
    public int Id { get; set; }
    
    public int OwnerId { get; set; }
    
    public List<ShoppingCartItem> Products { get; set; } = new List<ShoppingCartItem>();

    public decimal TotalPrice { get; set; }
}