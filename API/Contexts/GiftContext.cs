using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Contexts
{
    public class GiftContext : DbContext
    {
        public GiftContext(DbContextOptions<GiftContext> options) : base(options)
        {
        }
        
    }
}