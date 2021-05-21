using Microsoft.EntityFrameworkCore;

namespace MMStock.Data
{
    public class StockContext : DbContext
    {
        public StockContext(DbContextOptions<StockContext> options) : base(options)
        {
        }

        public DbSet<Stock> Stock { get; set; }
    }
}