using System.Collections;
using System.Collections.Generic;
using System.Linq;
using API.Entities;

namespace API.Services
{
    public interface ICategoryRepository
    {
        /// <summary>
        /// Get all categories from database
        /// </summary>
        /// <returns>ICollection of categories</returns>
        ICollection<Category> GetCategories();
        /// <summary>
        /// Get category from database by id
        /// </summary>
        /// <param name="id">category id</param>
        /// <returns>Category found in database</returns>
        Category GetCategoryById(int id);
        /// <summary>
        /// Get all products for a category by category id
        /// </summary>
        /// <param name="id">Category id</param>
        /// <returns>All products from that category</returns>
        IQueryable<Product> GetProductsByCategoryId(int id);
        /// <summary>
        /// Add category in database
        /// </summary>
        /// <param name="category">Category entity to add in db</param>
        void AddCategory(Category category);
        /// <summary>
        /// Update category in database
        /// </summary>
        /// <param name="id">Category id</param>
        /// <param name="category">Category entity</param>
        void UpdateCategory(int id, Category category);
        /// <summary>
        /// Partially update category in database
        /// </summary>
        /// <param name="category">Category entity</param>
        void PartiallyUpdateCategory(Category category);
        /// <summary>
        /// Delete category from database
        /// </summary>
        /// <param name="id">Category id</param>
        void DeleteCategory(int id);
    }
}