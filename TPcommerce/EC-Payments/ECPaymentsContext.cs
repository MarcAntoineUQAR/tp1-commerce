using EC_Payments.Models;
using Microsoft.EntityFrameworkCore;

namespace EC_Payments;

public class ECPaymentsContext : DbContext
{
    public DbSet<PaymentInfos> PaymentInfos { get; set; }

    string connectionString = "server=localhost;port=8080;database=dbpayments;user=root;password=admin123*;";


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

        modelBuilder.Entity<PaymentInfos>().HasData(
            new PaymentInfos()
            {
                Id = 1,
                CardNumber = "12345678",
                CardType = "Visa",
                CVV = "133",
                ExpirationDate = "12/25",
                FullName = "Yacine Yaddaden"
            });
    }
}