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
        
        public ICollection<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public Category GetCategoryById(int id)
        {
            return _context.Categories.Find(id);
        }

        public IQueryable<Product> GetProductsByCategoryId(int id)
        {
            return _context.Products.Where(p => p.CategoryId == id);
        }

        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void UpdateCategory(int id, Category category)
        {
            var categoryToUpdate = _context.Categories.Find(id);

            categoryToUpdate.Name = category.Name;
            categoryToUpdate.Description = category.Description;

            _context.Update(categoryToUpdate);
            _context.SaveChanges();
        }

        public void PartiallyUpdateCategory(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
        }

        public void DeleteCategory(int id)
        {
            var categoryToDelete = _context.Categories.Find(id);
            _context.Remove(categoryToDelete);
            _context.SaveChanges();
        }
    }
}