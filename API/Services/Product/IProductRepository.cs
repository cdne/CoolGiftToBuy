using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using API.Entities;

namespace API.Services
{
    public interface IProductRepository
    {
        /// <summary>
        /// Get all products from database using search, sort or no queries
        /// </summary>
        /// <param name="name">Search query by name</param>
        /// <param name="description">Search query by description</param>
        /// <param name="sortName">Sort name asc or desc</param>
        /// <returns>Collection of products based on search, sort and non queries</returns>
        ICollection<Product> GetProducts(string name, string description, string sortName);
        
        /// <summary>
        /// Get product from database by id
        /// </summary>
        /// <param name="id">Product id</param>
        /// <returns>Product entity from database</returns>
        Product GetProductById(int id);

        /// <summary>
        /// Add product in database
        /// </summary>
        /// <param name="product">Product to add in database</param>
        void AddProduct(Product product);
        IQueryable<Tag> GetProductTags(int productId);

        /// <summary>
        /// Update product in database
        /// </summary>
        /// <param name="id">Product id</param>
        /// <param name="product">Product entity to update</param>
        void UpdateProduct(int id, Product product);

        /// <summary>
        /// Partially update product in database
        /// </summary>
        /// <param name="product">Product entity to update</param>
        void Update(Product product);

        /// <summary>
        /// Delete product by id from database
        /// </summary>
        /// <param name="id">id of the product</param>
        void DeleteProductFromDatabase(int id);
        
        /// <summary>
        /// Get all tags for a product by product id
        /// </summary>
        /// <param name="productId">Product id</param>
        /// <returns>All tags for that product</returns>
        IQueryable<Tag> GetTagsByProductId(int productId);


    }
}