using TPcommerce.Models;

namespace TPcommerce.Repository;

public class BaseRepository
{
    private ProductRepository _productRepository;

    public BaseRepository(ProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public void PopulateDbContext()
    {
        PopulateUser();
        PopulateProduct();
    }

    private void PopulateUser()
    {
        TpcommerceContext context = new TpcommerceContext();
        if (!context.Users.Any())
        {
            var seller = new User();
            seller.Id = 1;
            seller.Username = "admin";
            seller.Password = "admin123*";
            seller.Role = "seller";
            context.Users.Add(seller);
            context.SaveChanges();
            var buyer = new User();
            buyer.Id = 2;
            buyer.Username = "buyer";
            buyer.Password = "buyer123*";
            buyer.Role = "buyer";
            context.Users.Add(buyer);
            context.SaveChanges();
        }
    }

    private void PopulateProduct()
    {
        TpcommerceContext context = new TpcommerceContext();
        if (!context.Products.Any())
        {
            var productList = _productRepository.GetProductsFromAPIRest().Result;
            context.Products.AddRange(productList);
            context.SaveChanges();
        }
    }
}