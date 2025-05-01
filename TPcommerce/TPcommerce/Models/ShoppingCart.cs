using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TPcommerce.Models.DTO;
 
 namespace TPcommerce.Models;
 
 public class ShoppingCart
 {
     public int Id { get; set; }
     public int OwnerId { get; set; }
     
     public User Owner { get; set; }
     
     [JsonPropertyName("items")]
     public List<ShoppingCartItem> ShoppingCartItems { get; set; } = new List<ShoppingCartItem>();
     
     [NotMapped]
     public decimal TotalPrice { get; set; }
 }