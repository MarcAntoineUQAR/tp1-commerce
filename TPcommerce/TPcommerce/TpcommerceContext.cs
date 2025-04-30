using Microsoft.EntityFrameworkCore;
using TPcommerce.Models;

namespace TPcommerce
{
    public class TpcommerceContext : DbContext
    {
        string connectionString = "server=localhost;port=3306;database=tpcommerce;user=root;password=admin123*;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 36));
            optionsBuilder.UseMySql(connectionString, serverVersion);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BillItem>().ToTable("BillItem");

            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<ShoppingCartItem>()
                .HasOne(sci => sci.ShoppingCart)
                .WithMany(s => s.ShoppingCartItems)
                .HasForeignKey(sci => sci.ShoppingCartId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ShoppingCartItem>()
                .HasOne(sci => sci.Product)
                .WithMany()
                .HasForeignKey(sci => sci.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}