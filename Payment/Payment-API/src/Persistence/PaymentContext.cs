using Microsoft.EntityFrameworkCore;
using Payment_API.src.Models;

namespace Payment_API.src.Persistence
{
    public class PaymentContext : DbContext
    {
        public PaymentContext(DbContextOptions<PaymentContext> options) : base(options)
        {
            
        }

        public DbSet<Sale> Sales { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Sale>(table => 
            {
                table.HasKey(p => p.Id);
                table.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
                table.HasMany(p => p.Products).WithOne();
            });

            builder.Entity<Seller>(table => 
            {
                table.HasKey(p => p.Id);
                table.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
                table.HasMany(p => p.Sales).WithOne(p => p.Seller);           
            });

            builder.Entity<Product>(table => 
            {
                table.HasKey(p => p.Id);
                table.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
                table.Property(p => p.Item).IsRequired();
            });
        }
    }
}