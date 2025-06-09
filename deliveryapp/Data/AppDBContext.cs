using deliveryapp.Models;
using Microsoft.EntityFrameworkCore;

namespace deliveryapp.Data
{
    public class AppDBContext : DbContext
    {
        public DbSet<Usuario> Users { get; set; } 
        public  DbSet<Roles> Roles { get; set; }
        public DbSet<Product> Products  { get; set; }

        public DbSet<Restaurant> Restaurants { get; set; }
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Roles>().HasData(
               new Roles {Id = 1, name = "Admin", description = "Admin role"},
               new Roles { Id = 2, name = "User", description = "Regular user role" }
            );
            base.OnModelCreating(modelBuilder);
        }

    }
}
