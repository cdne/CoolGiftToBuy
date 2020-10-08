using System.Collections.Generic;
using System.Linq;
using API.Contexts;
using API.Entities;

namespace API.Services
{
    public class ProductTagRepository : IProductTagRepository
    {
        private readonly GiftContext _context;

        public ProductTagRepository(GiftContext context)
        {
            _context = context;
        }

        public IQueryable<Tag> GetTagsByProductId(int productId)
        {
            return _context.Tags.Where(t => t.ProductId == productId);
        }

        public Tag GetTagById(int id)
        {
            return _context.Tags.Find(id);
        }
    }
}