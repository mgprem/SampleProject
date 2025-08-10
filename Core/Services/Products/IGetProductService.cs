using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Core.Services.Users
{
    public interface IGetProductService
    {
        IEnumerable<Product> GetAllProduct();
        Product GetProductById(Guid id);

        IEnumerable<Product> GetProducts(  string name = null, decimal? price = null);
       // IEnumerable<User> GetUsersByTag(string tag);
    }
}