using Catalog.Core.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Infrastructure.Data.Contexts
{
    public interface ICatalogCaontext
    {
        IMongoCollection<Product> Products { get; }
        IMongoCollection<ProductType> Types { get; }
        IMongoCollection<ProductBrand> Brands { get; }
    }
}
