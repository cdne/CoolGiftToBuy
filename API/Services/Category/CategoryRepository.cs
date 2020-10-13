using System.Collections.Generic;
using System.Linq;
using API.Contexts;
using API.Entities;

namespace API.Services
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly GiftContext _context;

        public CategoryRepository(GiftContext context)
        {
            _context = context;
        }
        
        /// <inheritdoc cref="ICategoryRepository"/>
        public ICollection<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        /// <inheritdoc cref="ICategoryRepository"/>
        public Category GetCategoryById(int id)
        {
            return _context.Categories.Find(id);
        }
        
        /// <inheritdoc cref="ICategoryRepository"/>
        public IQueryable<Product> GetProductsByCategoryId(int id)
        {
            return _context.Products.Where(p => p.CategoryId == id);
        }

        /// <inheritdoc cref="ICategoryRepository"/>
        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        /// <inheritdoc cref="ICategoryRepository"/>
        public void UpdateCategory(int id, Category category)
        {
            var categoryToUpdate = _context.Categories.Find(id);

            categoryToUpdate.Name = category.Name;
            categoryToUpdate.Description = category.Description;

            _context.Update(categoryToUpdate);
            _context.SaveChanges();
        }

        /// <inheritdoc cref="ICategoryRepository"/>
        public void PartiallyUpdateCategory(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
        }

        /// <inheritdoc cref="ICategoryRepository"/>
        public void DeleteCategory(int id)
        {
            var categoryToDelete = _context.Categories.Find(id);
            _context.Remove(categoryToDelete);
            _context.SaveChanges();
        }
    }
}