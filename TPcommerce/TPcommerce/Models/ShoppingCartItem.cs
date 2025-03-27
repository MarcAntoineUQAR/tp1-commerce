namespace TPcommerce.Models;

public class ShoppingCartItem
{
    public int Id { get; set; } // Clé primaire
    public int ShoppingCartId { get; set; } = -1; // Clé étrangère vers ShoppingCart
    public ShoppingCart ShoppingCart { get; set; } // Relation avec ShoppingCart
    public int ProductId { get; set; } // Clé étrangère vers Product
    public Product Product { get; set; } // Relation avec Product
    public int Quantity { get; set; } // Quantité du produit
}