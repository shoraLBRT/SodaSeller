using Microsoft.EntityFrameworkCore;
using SodaSeller.Models;

namespace SodaSeller.DAL
{
    public class SodaSellerContext : DbContext
    {
        public SodaSellerContext(DbContextOptions<SodaSellerContext> options) : base(options) { }

        public DbSet<SodaProducts> SodaProducts { get; set; }
        public DbSet<MachineCoins> MachineCoins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MachineCoins>().ToTable("MachineCoins");
            modelBuilder.Entity<MachineCoins>().ToTable(m => m.HasCheckConstraint("ValidCount", "Count >= 0"));

            modelBuilder.Entity<SodaProducts>().ToTable("SodaProducts");
            modelBuilder.Entity<SodaProducts>()
                .Property(s => s.ProductImage)
                .HasDefaultValue("https://topzero.com/wp-content/uploads/2020/06/topzero-products-Malmo-Matte-Black-TZ-PE458M-image-003.jpg");

            modelBuilder.Entity<SodaProducts>()
                .ToTable(c => c.HasCheckConstraint("ValidCount", "ProductCount >= 0"));
        }
    }
}
