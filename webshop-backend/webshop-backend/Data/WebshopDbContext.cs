using Microsoft.EntityFrameworkCore;
using webshop_backend.Models;

namespace webshop_backend.Data
{
    public class WebshopDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public WebshopDbContext(DbContextOptions<WebshopDbContext> options)
            : base(options)
        {
        }
    }
}
