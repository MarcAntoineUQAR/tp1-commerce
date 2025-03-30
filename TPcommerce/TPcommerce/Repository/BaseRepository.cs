using TPcommerce.Models;
using TPcommerce.Models.DTO;

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
        PopulateProduct();
        PopulateUser();
    }

    private void PopulateUser()
    {
        TpcommerceContext context = new TpcommerceContext();
        if (!context.Users.Any())
        {
            var sellerShoppingCart = new ShoppingCart();
            var seller = new User
            {
                Username = "admin",
                Password = "admin123*",
                Role = "seller",
                ShoppingCart = sellerShoppingCart
            };
            sellerShoppingCart.Owner = seller;

            context.Users.Add(seller);
            context.SaveChanges();

            var shoppingCartItem = new ShoppingCartItem
            {
                ProductId = 1,
                Quantity = 2,
                ShoppingCartId = sellerShoppingCart.Id
            };
            context.ShoppingCartItems.Add(shoppingCartItem);

            var buyerShoppingCart = new ShoppingCart();
            var buyer = new User
            {
                Username = "buyer",
                Password = "buyer123*",
                Role = "buyer",
                ShoppingCart = buyerShoppingCart
            };
            buyerShoppingCart.Owner = buyer;

            context.Users.Add(buyer);
            context.SaveChanges();

            var shoppingCartItem2 = new ShoppingCartItem
            {
                ProductId = 2,
                Quantity = 5,
                ShoppingCartId = buyerShoppingCart.Id
            };
            var shoppingCartItem3 = new ShoppingCartItem
            {
                ProductId = 19,
                Quantity = 3,
                ShoppingCartId = buyerShoppingCart.Id
            };
            context.ShoppingCartItems.AddRange(shoppingCartItem2, shoppingCartItem3);
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