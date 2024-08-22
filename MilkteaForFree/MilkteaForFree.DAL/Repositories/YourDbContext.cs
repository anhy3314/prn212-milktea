using Microsoft.EntityFrameworkCore;
using MilkteaForFree.DAL.Entities;

public class YourDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Drink> Drinks { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Configure your database connection here
        optionsBuilder.UseSqlServer("your_connection_string_here");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>()
            .HasMany(o => o.OrderDetails)
            .WithOne()
            .HasForeignKey(od => od.OrderId);

        modelBuilder.Entity<Drink>()
            .HasMany(d => d.OrderDetails)
            .WithOne()
            .HasForeignKey(od => od.DrinkId);

        // Additional configuration if needed
    }
}
