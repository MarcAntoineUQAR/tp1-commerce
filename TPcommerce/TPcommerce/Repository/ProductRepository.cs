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

        public GenericResponse<List<Product>> GetProductsBySearchTerm(string searchTerm)
        {
            try
            {
                TpcommerceContext context = new TpcommerceContext();
                var list = context.Products.Where(p => p.Title.Contains(searchTerm) || p.Category.Contains(searchTerm))
                    .OrderBy(s => s.Id).ToList();
                return new GenericResponse<List<Product>>(list, "rechercher reussite", true);
            }
            catch (Exception e)
            {
                return new GenericResponse<List<Product>>("Erreur survenu:" + e, false);
            }
        }

        public GenericResponse<string> CreateProduct(Product product)
        {
            try
            {
                TpcommerceContext context = new TpcommerceContext();
                context.Products.Add(product);
                context.SaveChanges();
                return new GenericResponse<string>("Product created successfully", true);
            }
            catch (Exception e)
            {
                return new GenericResponse<string>("Erreur survenu:" + e, false);
            }
        }

        public GenericResponse<Product> GetSingleProduct(int id)
        {
            try
            {
                TpcommerceContext context = new TpcommerceContext();
                var product = context.Products.Find(id);
                return new GenericResponse<Product>(product!, "reussi", true);
            }
            catch (Exception e)
            {
                return new GenericResponse<Product>("Erreur survenu:" + e, false);
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
                return new List<Product>();
            }
        }
    }
}