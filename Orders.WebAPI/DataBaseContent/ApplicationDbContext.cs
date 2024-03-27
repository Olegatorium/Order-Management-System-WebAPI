using Microsoft.EntityFrameworkCore;
using Orders.WebAPI.Models;

namespace Orders.WebAPI.DataBaseContent
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public ApplicationDbContext() { }

        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>()
                .HasIndex(o => o.OrderNumber)
                .IsUnique();


            modelBuilder.Entity<Order>().HasData(
              new Order
              {
                 OrderId = Guid.Parse("39B5E8EF-0F83-450F-A037-83720976999F"),
                 OrderNumber = "Order_2024_1",
                 CustomerName = "John Doe",
                 OrderDate = DateTime.UtcNow,
                 TotalAmount = 1777.00m
              }
            );

            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem
                {
                    OrderItemId = Guid.Parse("04942D7C-4D8A-49EA-8F78-F3B9FB8E0764"),
                    OrderId = Guid.Parse("39B5E8EF-0F83-450F-A037-83720976999F"),
                    ProductName = "Iphone 14",
                    Quantity = 2,
                    UnitPrice = 785.00m
                },
                new OrderItem
                {
                    OrderItemId = Guid.Parse("3E1D04B2-2ABA-43BD-9D6B-ED383ABD8771"),
                    OrderId = Guid.Parse("39B5E8EF-0F83-450F-A037-83720976999F"),
                    ProductName = "Microwave",
                    Quantity = 3,
                    UnitPrice = 69.00m
                }
            );

        }

    }
}
