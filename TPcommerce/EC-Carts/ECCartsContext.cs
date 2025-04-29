using EC_Carts.Models;
using Microsoft.EntityFrameworkCore;

namespace EC_Carts;

public class ECCartsContext : DbContext
{
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

    private readonly string connectionString = "server=localhost;port=8080;database=dbshoppingcarts;user=root;password=admin123*;";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ShoppingCartItem>()
            .HasOne(item => item.ShoppingCart)
            .WithMany(cart => cart.Items)
            .HasForeignKey(item => item.ShoppingCartId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}