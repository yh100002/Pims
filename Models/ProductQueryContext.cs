using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class ProductQueryContext : DbContext
    {
        public ProductQueryContext(DbContextOptions<ProductQueryContext> options) : base(options)
        {
        }

        public DbSet<ProductData> ProductData { get; set; }                
    }
}