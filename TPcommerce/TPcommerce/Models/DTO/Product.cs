using Newtonsoft.Json;
using TPcommerce.Models.DTO;

namespace TPcommerce.Models;

public class Product
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public decimal Price { get; set; }
    
    public string Image { get; set; }

    [JsonProperty("images")]
    private List<string> ImagesList
    {
        set => Image = (value != null && value.Count > 0) ? value[0] : "default-image.jpg";
    }
    
    public string Category { get; set; }
    public int? SellerId { get; set; }
    public User Seller { get; set; }
}

public class ProductResponse
{
    public List<Product> Products { get; set; }
}