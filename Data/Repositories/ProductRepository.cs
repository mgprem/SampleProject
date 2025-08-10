using System;
using System.Collections.Generic;
using System.Linq;
using BusinessEntities;
using Common;
using Data.Indexes;
using Raven.Client;
using Raven.Client.Document;

namespace Data.Repositories
{
    [AutoRegister]
    public class ProductRepository : ProductBaseRepository<Product>, IProductRepository
    {
        public ProductRepository() : base(InMemoryDatabase.Products, p => p.Id)
        {

        }

        public void DeleteAll()
        {
            DeleteAllInMemory();
        }
        public IEnumerable<Product> GetAllProduct()
        {
            
            return GetAllInMemory();
        }
        public IEnumerable<Product> Get(string name = null)
        {
            var items = GetAllInMemory();
            if (string.IsNullOrWhiteSpace(name)) return items;

            var nameLcase = name.ToLowerInvariant();
            return items.Where(p => !string.IsNullOrEmpty(p.Name) &&
                                    p.Name.ToLowerInvariant().Contains(nameLcase));
        }

        public Product GetProductById(Guid id)
        {
            
            return base.Get(id);

        }

        IEnumerable<Product> IProductRepository.GetByTag(string tag)
        {
            throw new NotImplementedException();
        }
    }
}