using Microsoft.EntityFrameworkCore;
using TPcommerce.Models;
using TPcommerce.Models.DTO;

namespace TPcommerce
{
    public class TpcommerceContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<PaymentInfos> PaymentInfos { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillItem> BillItems { get; set; }

        string connectionString = "server=localhost;port=8080;database=tpcommerce;user=root;password=admin123*;";
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BillItem>().ToTable("BillItem");
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(u => u.ShoppingCart)
                .WithOne(s => s.Owner)
                .HasForeignKey<ShoppingCart>(s => s.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ShoppingCartItem>()
                .HasOne(sci => sci.ShoppingCart)
                .WithMany(s => s.ShoppingCartItems)
                .HasForeignKey(sci => sci.ShoppingCartId);

            modelBuilder.Entity<ShoppingCartItem>()
                .HasOne(sci => sci.Product)
                .WithMany()
                .HasForeignKey(sci => sci.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}