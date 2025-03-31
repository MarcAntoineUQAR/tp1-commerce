using System.ComponentModel.DataAnnotations.Schema;
using TPcommerce.Models.DTO;

namespace TPcommerce.Models;

public class ShoppingCart
{
    public int Id { get; set; }
    public int OwnerId { get; set; }
    
    public User Owner { get; set; }
    public List<ShoppingCartItem> ShoppingCartItems { get; set; } = new List<ShoppingCartItem>();
    
    [NotMapped]
    public decimal TotalPrice { get; set; }
}