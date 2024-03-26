using KafeshkaV2.DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace KafeshkaV2.Infrastructure;

public class RestaurantDbContext : DbContext
{
    // DbSet for Customer entity
    public DbSet<Customer> Customers { get; set; }

    // DbSet for Item entity
    public DbSet<Item> Items { get; set; }

    // DbSet for Order entity
    public DbSet<Order> Orders { get; set; }

    // DbSet for OrderItems entity
    public DbSet<OrderItems> OrderItems { get; set; }

    public RestaurantDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Add any additional configuration or relationships here if needed
    }
}