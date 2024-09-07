using EcommerceAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.DbContext;

public class ApplicationContext : Microsoft.EntityFrameworkCore.DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

    public DbSet<Cart> Carts { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasPrecision(18, 2);

        base.OnModelCreating(modelBuilder);
    }
}
