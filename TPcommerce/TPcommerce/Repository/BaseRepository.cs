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
        PopulateUser();
        PopulateProduct();
    }

    private void PopulateUser()
    {
        TpcommerceContext context = new TpcommerceContext();
    
        if (!context.Users.Any())
        {
            // Créer un panier pour le vendeur
            var sellerShoppingCart = new ShoppingCart();
            var seller = new User()
            {
                Username = "admin",
                Password = "admin123*",
                Role = "seller",
                ShoppingCart = sellerShoppingCart  // Associer le panier à l'utilisateur
            };

            sellerShoppingCart.Owner = seller;  // Associer l'utilisateur au panier
            context.ShoppingCarts.Add(sellerShoppingCart);  // Ajouter le panier d'abord
            context.Users.Add(seller);  // Ajouter l'utilisateur ensuite

            // Créer un panier pour l'acheteur
            var buyerShoppingCart = new ShoppingCart();  // Créer le panier d'abord
            var buyer = new User()
            {
                Username = "buyer",
                Password = "buyer123*",
                Role = "buyer",
                ShoppingCart = buyerShoppingCart  // Associer le panier à l'utilisateur
            };
            buyerShoppingCart.Owner = buyer;  // Associer l'utilisateur au panier
            context.ShoppingCarts.Add(buyerShoppingCart);  // Ajouter le panier d'abord
            context.Users.Add(buyer);  // Ajouter l'utilisateur ensuite

            // Sauvegarder les modifications dans la base de données
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