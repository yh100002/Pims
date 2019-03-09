using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class ProductReaderContext : DbContext
    {
        public ProductReaderContext(DbContextOptions<ProductReaderContext> options) : base(options)
        {
        }

        public DbSet<ProductData> ProductData { get; set; }                
    }
}