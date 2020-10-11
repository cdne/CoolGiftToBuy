using System.Collections;
using System.Collections.Generic;
using System.Linq;
using API.Entities;

namespace API.Services
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategoryById(int id);
        IQueryable<Product> GetProductsByCategoryId(int id);
        void AddCategory(Category category);
        void UpdateCategory(int id, Category category);
    }
}