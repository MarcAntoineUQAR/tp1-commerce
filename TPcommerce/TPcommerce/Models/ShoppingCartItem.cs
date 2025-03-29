namespace TPcommerce.Models;

public class ShoppingCartItem
{
    public int Id { get; set; }
    public int ShoppingCartId { get; set; }
    public ShoppingCart ShoppingCart { get; set; } // Relation avec ShoppingCart
    public int ProductId { get; set; }
    public Product Product { get; set; } // Relation avec Product
    public int Quantity { get; set; }
}