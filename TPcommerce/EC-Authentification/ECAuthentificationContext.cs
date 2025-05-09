using EC_Authentification.Models;
using Microsoft.EntityFrameworkCore;

namespace EC_Authentification;

public class EcAuthentificationContext : DbContext
{
    public DbSet<LoginRequest> Authentifications { get; set; }

    string connectionString = "server=localhost;port=8080;database=dbauthentification;user=root;password=admin123*;";


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
}