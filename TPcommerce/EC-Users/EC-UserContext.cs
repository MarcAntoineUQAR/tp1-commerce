using EC_Users.Models;
using Microsoft.EntityFrameworkCore;

namespace EC_Users
{
    public class EcUserContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        string connectionString = "server=localhost;port=3306;database=dbusers;user=root;password=admin123*;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 36));

            optionsBuilder.UseMySql(connectionString, serverVersion);
        }
    }
}