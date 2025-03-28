using Newtonsoft.Json;
using TPcommerce.Models;

namespace TPcommerce.Repository
{
    public class ProductRepository
    {
        private readonly HttpClient _client;

        public ProductRepository()
        {
            _client = new HttpClient();
        }

        public GenericResponse<List<Product>> GetProducts()
        {
            try
            {
                TpcommerceContext context = new TpcommerceContext();
                var list = context.Products.ToList();
                return new GenericResponse<List<Product>>(list, "reussi", true);
            }
            catch (Exception e)
            {
                return new GenericResponse<List<Product>>("Error getting products: " + e, false);
            }
        }

        public async Task<List<Product>> GetProductsFromAPIRest()
        {
            try
            {
                var response = await _client.GetStringAsync("https://dummyjson.com/products");

                var productResponse = JsonConvert.DeserializeObject<ProductResponse>(response);

                return productResponse?.Products ?? new List<Product>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching products: {ex.Message}");
                return new List<Product>();
            }
        }
    }
}