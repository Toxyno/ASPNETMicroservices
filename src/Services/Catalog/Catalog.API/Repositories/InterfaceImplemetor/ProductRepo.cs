﻿using Catalog.API.Data.Interface;
using Catalog.API.Entities;
using Catalog.API.Repositories.Interface;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Repositories.InterfaceImplemetor
{
    public class ProductRepo : IProductRepo
    {
        private readonly ICatalogContext _context;
        private readonly ILogger<ProductRepo> _logger;

        public ProductRepo(ICatalogContext context, ILogger<ProductRepo> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task CreateProduct(Product product)
        {
            await _context.Products.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            var deleteResult = await _context
                                         .Products
                                         .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                  && deleteResult.DeletedCount > 0;
        }

        public async Task<Product> GetProduct(string id)
        {
            return await _context.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string CategoryName)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category,CategoryName);
            return await _context.Products.Find(filter).ToListAsync();
            //return await _context.Products.Find(p => p.Category == CategoryName).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, name);
            //return await _context.Products.Find(p => p.Name == name).ToListAsync();
            return await _context.Products.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products.Find(p => true).ToListAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var updateResult = await _context
                                         .Products
                                         .ReplaceOneAsync(filter:p => p.Id == product.Id, replacement: product);

            return updateResult.IsAcknowledged
                  && updateResult.ModifiedCount > 0;

        }
    }
}
