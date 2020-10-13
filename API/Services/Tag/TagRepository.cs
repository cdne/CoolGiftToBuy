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
        
        /// <inheritdoc cref="ITagRepository"/>
        public ICollection<Tag> GetAllTags()
        {
            return _context.Tags.ToList();
        }

        /// <inheritdoc cref="ITagRepository"/>
        public Tag GetTagById(int id)
        {
            return _context.Tags.Find(id);
        }
        
        /// <inheritdoc cref="ITagRepository"/>
        public void Add(Tag tag)
        {
            _context.Tags.Add(tag);
            _context.SaveChanges();
        }
        
        /// <inheritdoc cref="ITagRepository"/>
        public void Update(int id, Tag tag)
        {
            var tagToUpdate = GetTagById(id);
            tagToUpdate.Name = tag.Name;
            _context.Tags.Update(tagToUpdate);
            _context.SaveChanges();
        }
        
        /// <inheritdoc cref="ITagRepository"/>
        public void PartiallyUpdate(Tag tag)
        {
            _context.Tags.Update(tag);
            _context.SaveChanges();
        }
        
        /// <inheritdoc cref="ITagRepository"/>
        public void Delete(Tag tag)
        {
            _context.Tags.Remove(tag);
            _context.SaveChanges();
        }
    }
}