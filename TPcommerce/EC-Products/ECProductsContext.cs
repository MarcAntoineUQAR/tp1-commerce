using EC_Products.Models;
using Microsoft.EntityFrameworkCore;

namespace EC_Products;

public class ECProductsContext : DbContext
{
    public DbSet<Product> Product { get; set; }

    string connectionString = "server=localhost;port=8080;database=dbproducts;user=root;password=admin123*;";


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>().HasData(
            new Product()
            {
                Id = 1,
                Category = "Electronics",
                Description = "Electronics Electronics",
                Image = "electronics.jpg",
                Price = (decimal)10.00,
                SellerId = 1,
                Title = "Cellphone"
            },
            new Product()
            {
                Id = 2,
                Category = "Meals",
                Description = "Pizza peperonni",
                Image = "meals.jpg",
                Price = (decimal)15.00,
                SellerId = 1,
                Title = "Pizza"
            });
    }
}