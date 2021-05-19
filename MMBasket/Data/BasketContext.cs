using Microsoft.EntityFrameworkCore;

namespace MMBasket.Data
{
    public class BasketContext : DbContext
    {
        public BasketContext(DbContextOptions<BasketContext> options) : base(options)
        {
            
        }

        public DbSet<Basket> Baskets { get; set; }
    }
}