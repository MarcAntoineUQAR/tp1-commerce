using TPcommerce.Models.DTO;

namespace TPcommerce.Models;

public class ShoppingCart
{
    public int Id { get; set; }
    public int OwnerId { get; set; }  // Clé étrangère vers User
    public User Owner { get; set; }   // Relation avec User
    public List<ShoppingCartItem> Products { get; set; } = new List<ShoppingCartItem>();
    public decimal TotalPrice { get; set; }
}