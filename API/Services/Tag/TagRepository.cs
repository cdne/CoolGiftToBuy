using System.Collections.Generic;
using System.Linq;
using API.Contexts;
using API.Entities;
using API.Models;

namespace API.Services
{
    public class TagRepository : ITagRepository
    {
        private readonly GiftContext _context;

        public TagRepository(GiftContext context)
        {
            _context = context;
        }
        
        public ICollection<Tag> GetAllTags()
        {
            return _context.Tags.ToList();
        }

        public Tag GetTagById(int id)
        {
            return _context.Tags.Find(id);
        }

        public IQueryable<ProductDto> GetAllProductsByTag(int tagId)
        {
            throw new System.NotImplementedException();
        }
        
    }
}