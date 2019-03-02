using Microsoft.EntityFrameworkCore;


namespace Models
{
    public class ProductCommandContext : DbContext
    {
        public ProductCommandContext(DbContextOptions<ProductCommandContext> options) : base(options)
        {
        }

        public DbSet<ProductData> ProductData { get; set; }        
    }
}