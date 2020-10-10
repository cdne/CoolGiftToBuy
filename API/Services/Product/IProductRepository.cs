using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using API.Entities;

namespace API.Services
{
    public interface IProductRepository
    {
        ICollection<Product> GetProducts();

        ICollection<Product> GetProducts(string name, string description, string sortName);
        
        Product GetProductById(int id);

        void AddProduct(Product product);
        IQueryable<Tag> GetProductTags(int productId);

        void UpdateProduct(int id, Product product);

    }
}