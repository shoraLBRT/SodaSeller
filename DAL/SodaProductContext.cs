using Microsoft.EntityFrameworkCore;
using SodaSeller.Models;

namespace SodaSeller.DAL
{
    public class SodaProductContext : DbContext
    {
        public SodaProductContext(DbContextOptions<SodaProductContext> options) : base(options) { }

        public DbSet<SodaProducts> SodaProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SodaProducts>().ToTable("SodaProducts");
            modelBuilder.Entity<SodaProducts>()
                .Property(s => s.ProductImage)
                .HasDefaultValue("https://topzero.com/wp-content/uploads/2020/06/topzero-products-Malmo-Matte-Black-TZ-PE458M-image-003.jpg");

            modelBuilder.Entity<SodaProducts>()
                .ToTable(c => c.HasCheckConstraint("ValidCount", "ProductCount >= 0"));
        }
    }
}
