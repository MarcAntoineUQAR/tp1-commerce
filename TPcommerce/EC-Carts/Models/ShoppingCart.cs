using System.ComponentModel.DataAnnotations.Schema;

namespace EC_Carts.Models;

public class ShoppingCart
{
    public int Id { get; set; }

    public int OwnerId { get; set; }

    public List<ShoppingCartItem> Items { get; set; } = new();
    
    [NotMapped]
    public decimal TotalPrice { get; set; }
}