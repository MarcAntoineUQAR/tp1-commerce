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
}