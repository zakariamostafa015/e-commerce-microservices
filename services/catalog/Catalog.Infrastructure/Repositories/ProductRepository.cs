using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Core.Specs;
using Catalog.Infrastructure.Data.Contexts;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository, IBrandRepository, ITypeRepository
    {
        private ICatalogCaontext _context { get; set; }
        public ProductRepository(ICatalogCaontext context)
        {
            _context = context;
        }

        public async Task<Pagination<Product>> GetAllProducts(CatalogSpecsParams catalogSpecsParams)
        {
            var filter = Builders<Product>.Filter.Empty;
            if (!string.IsNullOrEmpty(catalogSpecsParams.BrandId))
            {
                filter &= Builders<Product>.Filter.Eq(p => p.Brand.Id, catalogSpecsParams.BrandId);
            }
            if (!string.IsNullOrEmpty(catalogSpecsParams.TypeId))
            {
                filter &= Builders<Product>.Filter.Eq(p => p.Type.Id, catalogSpecsParams.TypeId);
            }
            if (!string.IsNullOrEmpty(catalogSpecsParams.Search))
            {
                filter &= Builders<Product>.Filter.Regex(p => p.Name, new MongoDB.Bson.BsonRegularExpression(catalogSpecsParams.Search, "i"));
            }

            var totalItems = await _context.Products.CountDocumentsAsync(filter);
            var data = await DataFilter(catalogSpecsParams, filter);

            return new Pagination<Product>(catalogSpecsParams.PageNumber, catalogSpecsParams.PageSize, (int)totalItems, data);
        }
        public async Task<Product> GetProductById(string id)
        {
            return await _context.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Product>> GetAllProductsByBrand(string name)
        {
            return await _context.Products.Find(p => p.Brand.Name == name).ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetAllProductsByName(string name)
        {
            return await _context.Products.Find(p => p.Name == name).ToListAsync();
        }
        public async Task<Product> CreateProduct(Product product)
        {
            await _context.Products.InsertOneAsync(product);
            return product;
        }

        public async Task<bool> DeleteProduct(string id)
        {
            var deletedProduct = await _context.Products.DeleteOneAsync(p => p.Id == id);
            return deletedProduct.IsAcknowledged && deletedProduct.DeletedCount > 0;
        }
        public async Task<bool> UpdateProduct(Product product)
        {
            var dupdatedProduct = await _context.Products.ReplaceOneAsync(p => p.Id == product.Id, product);
            return dupdatedProduct.IsAcknowledged && dupdatedProduct.ModifiedCount > 0;
        }
        public async Task<IEnumerable<ProductBrand>> GetAllBrands()
        {
            return await _context.Brands.Find(p => true).ToListAsync();
        }
        public async Task<IEnumerable<ProductType>> GetAllTypes()
        {
            return await _context.Types.Find(p => true).ToListAsync();
        }

        public async Task<ProductBrand?> GetBrandById(string id)
        {
            return await _context.Brands.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ProductType?> GetTypeById(string id)
        {
            return await _context.Types.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        private async Task<IReadOnlyList<Product>> DataFilter(CatalogSpecsParams catalogSpecsParams, FilterDefinition<Product> filter)
        {
            var sortDefinition = Builders<Product>.Sort.Ascending(p => p.Name);
            if(!string.IsNullOrEmpty(catalogSpecsParams.Sort))
            {
                sortDefinition = catalogSpecsParams.Sort switch
                {
                    "name_desc" => Builders<Product>.Sort.Descending(p => p.Name),
                    "price" => Builders<Product>.Sort.Ascending(p => p.Price),
                    "price_desc" => Builders<Product>.Sort.Descending(p => p.Price),
                    _ => sortDefinition
                };
            }

            return await _context.Products
                .Find(filter)
                .Sort(sortDefinition)
                .Skip(catalogSpecsParams.GetSkip())
                .Limit(catalogSpecsParams.GetTake())
                .ToListAsync();
        }
    }
}
