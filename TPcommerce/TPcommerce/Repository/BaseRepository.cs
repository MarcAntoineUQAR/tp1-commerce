using TPcommerce.Models;

namespace TPcommerce.Repository;

public class BaseRepository
{
    public void PeuplateDbContext()
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
}