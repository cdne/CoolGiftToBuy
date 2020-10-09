using System;
using System.Collections.Generic;
using System.Linq;
using API.Contexts;
using API.Entities;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class ProductRepository : IProductRepository
    {
        private readonly GiftContext _context;

        public ProductRepository(GiftContext context)
        {
            _context = context;
        }
        
        public ICollection<Product> GetProducts()
        {
           return _context.Products
               .OrderBy(p => p.Id)
               .ToList();
        }

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

        public Product GetProductById(int id)
        {
            return _context.Products.Find(id);
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public IQueryable<Tag> GetProductTags(int id)
        {
            return _context.Tags.Where(t => t.ProductId == id);
        }
    }
}