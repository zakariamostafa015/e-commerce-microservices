using Catalog.Core.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Infrastructure.Data.Contexts
{
    public class CatalogCaontext : ICatalogCaontext
    {
        public IMongoCollection<Product> Products { get; }
        public IMongoCollection<ProductType> Types { get; }
        public IMongoCollection<ProductBrand> Brands { get; }

        public CatalogCaontext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["DatabaseSettings:ConnectionString"]);
            var database = client.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);

            Products = database.GetCollection<Product>(configuration["DatabaseSettings:ProductsCollection"]);
            Types = database.GetCollection<ProductType>(configuration["DatabaseSettings:TypesCollection"]);
            Brands = database.GetCollection<ProductBrand>(configuration["DatabaseSettings:BrandsCollection"]);

            _= BrandContextSeed.SeedDataAsync(Brands);
            _= TypeContextSeed.SeedDataAsync(Types);
            _= ProductContextSeed.SeedDataAsync(Products);
        }
    }
}
