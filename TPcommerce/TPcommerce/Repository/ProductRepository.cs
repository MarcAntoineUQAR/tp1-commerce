using Newtonsoft.Json;
using TPcommerce.Models;

namespace TPcommerce.Repository
{
    public class ProductRepository
    {
        private readonly HttpClient _client;

        public ProductRepository(HttpClient client)
        {
            _client = client;
        }

        public GenericResponse<List<Product>> GetProducts()
        {
            // try
            // {
            //     TpcommerceContext context = new TpcommerceContext();
            //     var list = context.Products.ToList();
            //     return new GenericResponse<List<Product>>(list, "reussi", true);
            // }
            // catch (Exception e)
            // {
            //     return new GenericResponse<List<Product>>("Error getting products: " + e, false);
            // }
            return null;
        }

        public GenericResponse<List<Product>> GetProductsBySearchTerm(string searchTerm)
        {
            // try
            // {
            //     TpcommerceContext context = new TpcommerceContext();
            //     var list = context.Products.Where(p => p.Title.Contains(searchTerm) || p.Category.Contains(searchTerm))
            //         .OrderBy(s => s.Id).ToList();
            //     return new GenericResponse<List<Product>>(list, "rechercher reussite", true);
            // }
            // catch (Exception e)
            // {
            //     return new GenericResponse<List<Product>>("Erreur survenu:" + e, false);
            // }
            return null;
        }

        public GenericResponse<string> CreateProduct(Product product)
        {
            // try
            // {
            //     TpcommerceContext context = new TpcommerceContext();
            //     context.Products.Add(product);
            //     context.SaveChanges();
            //     return new GenericResponse<string>("Product created successfully", true);
            // }
            // catch (Exception e)
            // {
            //     return new GenericResponse<string>("Erreur survenu:" + e, false);
            // }
            return null;
        }

        public async Task<GenericResponse<Product>> GetSingleProduct(int id)
        {
            var product = await _client.GetFromJsonAsync<Product>($"http://localhost:5235/Product/{id}");
            return new GenericResponse<Product>(product, "Success", true);
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
                return new List<Product>();
            }
        }

        public async Task<GenericResponse<string>> PopulateDbContext()
        {
            var products = await GetProductsFromAPIRest();
            foreach (var product in products)
            {
                var response = _client.PostAsJsonAsync("http://localhost:5235/Product", product);
            }
            return new GenericResponse<string>("success", true);

        }
        
        public async Task<bool> HasExistingProducts()
        {
            try
            {
                var products = await _client.GetFromJsonAsync<List<Product>>("http://localhost:5235/Product");
                return products != null && products.Count > 0;
            }
            catch
            {
                return false;
            }
        }

    }
}