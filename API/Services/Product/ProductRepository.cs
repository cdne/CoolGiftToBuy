using System;
using System.Collections.Generic;
using System.Linq;
using API.Contexts;
using API.Entities;
using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Services
{
    public class ProductRepository : IProductRepository
    {
        private readonly GiftContext _context;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(GiftContext context, ILogger<ProductRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        
        /// <inheritdoc cref="IProductRepository"/>
        public ICollection<Product> GetProducts()
        {
           return _context.Products
               .OrderBy(p => p.Id)
               .ToList();
        }
        
        /// <inheritdoc cref="IProductRepository"/>
        public ICollection<Product> GetProducts(string name, string description, string sortName)
        {            
            var collection = _context.Products as IQueryable<Product>;
            
            if (string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(description) && string.IsNullOrWhiteSpace(sortName))
                return GetProducts();
            
            if (!string.IsNullOrWhiteSpace(sortName) && sortName.Equals("asc"))
            {
                collection = collection.OrderBy(p => p.Name);
                return collection.ToList();
            }
            else if(!string.IsNullOrWhiteSpace(sortName) && sortName.Equals("desc"))
            {
                collection = collection.OrderByDescending(p => p.Name);
                return collection.ToList();
            }


            if (!string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(description))
            {
                collection =  collection.Where(p => p.Name.ToLower().Contains(name.ToLower()));
                return collection.ToList();
            }

            if (string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(description))
            {
                collection = collection.Where(p => p.Description.ToLower().Contains(description.ToLower()));
                return collection.ToList();
            }

            collection = collection.Where(p => p.Name.ToLower().Contains(name.ToLower()))
                .Where(p => p.Description.ToLower().Contains(description.ToLower()));
            return collection.ToList();
        }
        
        /// <inheritdoc cref="IProductRepository"/>
        public Product GetProductById(int id)
        {
            return _context.Products.Find(id);
        }
        
        /// <inheritdoc cref="IProductRepository"/>
        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }
        
        /// <inheritdoc cref="IProductRepository"/>
        public IQueryable<Tag> GetProductTags(int id)
        {
            return _context.Tags.Where(t => t.ProductId == id);
        }
        
        /// <inheritdoc cref="IProductRepository"/>
        public void UpdateProduct(int id, Product product)
        {
            var productFromDatabase = _context.Products.Find(id);
            
            productFromDatabase.Description = product.Description;
            productFromDatabase.Name = product.Name;
            productFromDatabase.AffiliateLink = product.AffiliateLink;
            productFromDatabase.CategoryId = product.CategoryId;
            productFromDatabase.ImageSource = product.ImageSource;
            productFromDatabase.ProductUrl = product.ProductUrl;

            _context.Products.Update(productFromDatabase);
            _context.SaveChanges();
        }
        
        /// <inheritdoc cref="IProductRepository"/>
        public void Update(Product product)
        {
            _context.Update(product);
            _context.SaveChanges();
        }

        /// <inheritdoc cref="IProductRepository"/>
        public void DeleteProductFromDatabase(int id)
        {
            var productToDelete = _context.Products.Find(id);
            _context.Products.Remove(productToDelete);
            _context.SaveChanges();
        }
        
        /// <inheritdoc cref="IProductRepository"/>
        public IQueryable<Tag> GetTagsByProductId(int id)
        {
            return _context.Tags.Where(t => t.ProductId == id);
        }
    }
}