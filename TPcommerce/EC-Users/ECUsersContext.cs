using Microsoft.EntityFrameworkCore;

namespace EC_Users
{
    public class EcUsersContext(DbContextOptions<EcUsersContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; init; }

        private const string ConnectionString = "server=localhost;port=8080;database=dbusers;user=root;password=admin123*;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    Password = "admin123*",
                    Role = "Seller",
                    ShoppingCartId = 0
                },
                new User
                {
                    Id = 2,
                    Username = "buyer",
                    Password = "buyer123*",
                    Role = "Buyer",
                    ShoppingCartId = 1
                }
            );
        }
    }
}