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

        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Repartidor> Repartidor { get; set; }
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Roles>().HasData(
               new Roles {Id = 1, name = "Admin", description = "Admin role"},
               new Roles { Id = 2, name = "User", description = "Regular user role" }
            );

            modelBuilder.Entity<Usuario>().HasData(
                new Usuario { Id = 1, Names = "Admin", LastNames = "User", Email = "admin@mail.com",    Password = "admin123", IdRole = 1 },
                new Usuario { Id = 2, Names = "Regular", LastNames = "User", Email = "regular@mail.com", Password = "user123", IdRole = 2 }
            );

            modelBuilder.Entity<Restaurant>().HasData(
                new Restaurant { Id = 1, Name = "Restaurant A", Address = "123 Main St", PhoneNumber = "123-456-7890", Description= "Lorem Ipsum is simply dummy text of the printing and typesetting industry.", Email="restauranta@mail.com", ImageUrl="/images/restaurants/restauranta.jpg" },
                new Restaurant { Id = 2, Name = "Restaurant B", Address = "456 Elm St", PhoneNumber = "987-654-3210", Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry.", Email = "restaurantb@mail.com", ImageUrl="/images/restaurants/restaurantb.jpg" },
                new Restaurant { Id = 3, Name = "Restaurant C", Address = "789 Oak St", PhoneNumber = "555-555-5555", Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry.", Email = "restaurantc@mail.com", ImageUrl="/images/restaurants/restaurantc.jpg" },
                new Restaurant { Id = 4, Name = "Restaurant D", Address = "321 Pine St", PhoneNumber = "111-222-3333", Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry.", Email = "restaurantd@mail.com", ImageUrl="/images/restaurants/restaurantd.jpg" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Pizza", Description = "Delicious cheese pizza", Price = 9.99m, ImageUrl = "/images/food/pizza.webp", IdRestaurant = 1 },
                new Product { Id = 2, Name = "Hamburguesa", Description = "Juicy beef burger", Price = 8.99m, ImageUrl = "/images/food/burger.jpg", IdRestaurant = 2 },
                new Product { Id = 3, Name = "Pasta", Description = "Creamy Alfredo pasta", Price = 10.99m, ImageUrl = "/images/food/pasta.jpg", IdRestaurant = 3 },
                new Product { Id = 4, Name = "Ensalada", Description = "Fresh garden salad", Price = 7.99m, ImageUrl = "/images/food/salad.jpg", IdRestaurant = 4 },
                new Product { Id = 5, Name = "Sushi", Description = "Assorted sushi platter", Price = 12.99m, ImageUrl = "/images/food/sushi.jpg", IdRestaurant = 1 },
                new Product { Id = 6, Name = "Tacos", Description = "Spicy chicken tacos", Price = 6.99m, ImageUrl = "/images/food/tacos.jpg", IdRestaurant = 2 },
                new Product { Id = 7, Name = "Bistec", Description = "Grilled", Price = 15.99m, ImageUrl = "/images/food/steak.jpg", IdRestaurant = 3 },
                new Product { Id = 8, Name = "Helado de Vainilla", Description = "Vanilla ice cream with chocolate sauce", Price = 4.99m, ImageUrl = "/images/food/icecream.jpg", IdRestaurant = 4 },
                new Product { Id = 9, Name = "Arroz frito", Description = "Vegetable fried rice", Price = 7.99m, ImageUrl = "/images/food/friedrice.jpg", IdRestaurant = 1 },
                new Product { Id = 10, Name = "Ensalada Cesar", Description = "Classic Caesar salad with croutons", Price = 5.99m, ImageUrl = "/images/food/caesarsalad.jpg", IdRestaurant = 2 },
                new Product { Id = 11, Name = "Salmon a la parrilla", Description = "Salmon fillet with lemon butter sauce", Price = 14.99m, ImageUrl = "/images/food/grilledsalmon.jpg", IdRestaurant = 3 },
                new Product { Id = 12, Name = "Pastel de Chocolate", Description = "Rich chocolate cake with ganache", Price = 6.99m, ImageUrl = "/images/food/chocolatecake.jpg", IdRestaurant = 4 }

            );

            modelBuilder.Entity<Repartidor>().HasData(
                new Repartidor { Id = 1, Nombres = "Pedro", Apellidos="Juarez", Telefono = "972345712", status= "Disponible", Dni="87231476", ImageUrl="/images/repartidores/pedrojuarez.jpg"},
                new Repartidor { Id = 2, Nombres = "Maria", Apellidos = "Lopez", Telefono = "987654321", status = "Ocupado", Dni = "12345678", ImageUrl = "/images/repartidores/marialopez.jpg" },
                new Repartidor { Id = 3, Nombres = "Juan", Apellidos = "Perez", Telefono = "934728345", status = "offline", Dni = "87654321", ImageUrl = "/images/repartidores/juanperez.jpg" },
                new Repartidor { Id = 4, Nombres = "Ruben", Apellidos = "Gomez", Telefono = "907012734", status = "Disponible", Dni = "23456789", ImageUrl = "/images/repartidores/rubengomez.jpg" }
            );
            modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Product)
            .WithMany(p => p.OrderItems)
            .HasForeignKey(oi => oi.ProductId)
            .OnDelete(DeleteBehavior.NoAction);


            base.OnModelCreating(modelBuilder);
        }

    }
}
