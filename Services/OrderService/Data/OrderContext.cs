using Microsoft.EntityFrameworkCore;
using OrderService.Models;

namespace OrderService.Data
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ShippingAddress).IsRequired();
                entity.Property(e => e.BillingAddress).IsRequired();
                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ProductId).IsRequired();
                entity.Property(e => e.ProductName).IsRequired();
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18,2)");
            });

            // Seed data
            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    Id = 1,
                    UserId = 1,
                    OrderDate = DateTime.UtcNow.AddDays(-2),
                    TotalAmount = 1500.00m,
                    Status = OrderStatus.Processing,
                    ShippingAddress = "123 Main St, City, Country",
                    BillingAddress = "123 Main St, City, Country"
                }
            );

            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem
                {
                    Id = 1,
                    OrderId = 1,
                    ProductId = "602d2149e773f2a3990b47f5",
                    ProductName = "iPhone 15",
                    Quantity = 1,
                    Price = 950.00m,
                    TotalPrice = 950.00m
                },
                new OrderItem
                {
                    Id = 2,
                    OrderId = 1,
                    ProductId = "602d2149e773f2a3990b47f7",
                    ProductName = "MacBook Pro",
                    Quantity = 1,
                    Price = 550.00m,
                    TotalPrice = 550.00m
                }
            );
        }
    }
}