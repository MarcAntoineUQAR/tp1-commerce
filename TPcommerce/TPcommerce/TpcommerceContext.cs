using Microsoft.EntityFrameworkCore;
using TPcommerce.Models;

namespace TPcommerce
{
    public class TpcommerceContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<PaymentInfos> PaymentInfos { get; set; }
        public DbSet<Bill> Bills { get; set; }
        string connectionString = "server=localhost;port=8080;database=tpcommerce;user=root;password=admin123*;";
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<User>().HasData(
            //     new User() { Username = "admin", Password = "admin123*", Role = "seller", Id = 1 },
            //     new User() { Username = "clientTest", Role = "buyer", Password = "test", Id = 2, });
        }
    }
}