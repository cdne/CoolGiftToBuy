using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Contexts
{
    public class GiftContext : DbContext
    {
        public GiftContext(DbContextOptions<GiftContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Category> Categories { get; set; }
        
    }
}