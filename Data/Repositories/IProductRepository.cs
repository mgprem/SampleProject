using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Data.Repositories
{
    public interface IProductRepository : IProductBaseRepository<Product>
    {
        IEnumerable<Product> Get( string name = null);
        void DeleteAll();
        IEnumerable<Product> GetByTag(string tag);
        Product GetProductById(Guid id);
        IEnumerable<Product> GetAllProduct();
    }
}