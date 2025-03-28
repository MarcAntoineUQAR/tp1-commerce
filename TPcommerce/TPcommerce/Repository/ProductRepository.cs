using TPcommerce.Models;

namespace TPcommerce.Repository;

public class ProductRepository
{
    public void GetProductsFromAPIRest()
    {
        var client = new HttpClient();
        var response =  client.GetStringAsync("'https://dummyjson.com/products'").GetAwaiter().GetResult();
    }
}