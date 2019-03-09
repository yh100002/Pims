using Microsoft.EntityFrameworkCore;


namespace Models
{
    public class ProductWriterContext : DbContext
    {
        public ProductWriterContext(DbContextOptions<ProductWriterContext> options) : base(options)
        {
        }

        public DbSet<ProductData> ProductData { get; set; }        
    }
}