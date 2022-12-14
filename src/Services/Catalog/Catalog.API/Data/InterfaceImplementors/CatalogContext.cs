using Catalog.API.Data.Interface;
using Catalog.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Data.InterfaceImplementors
{
    public class CatalogContext : ICatalogContext
    {
        private readonly IConfiguration _config;

       
        public CatalogContext(IConfiguration config)
        {
            _config = config;
            //create a mongo client

            var client = new MongoClient(_config.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(_config.GetValue<string>("DatabaseSettings:DatabaseName"));

            //Populate the product Collection
            Products = database.GetCollection<Product>(_config.GetValue<string>("DatabaseSettings:CollectionName"));

            CatalogContextSeed.SeedData(Products);
        }
        public IMongoCollection<Product> Products{ get; }

    }
}
