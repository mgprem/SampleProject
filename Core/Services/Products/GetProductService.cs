using System;
using System.Collections.Generic;
using BusinessEntities;
using Common;
using Data.Repositories;

namespace Core.Services.Users
{
    [AutoRegister]
    public class GetProductService : IGetProductService
    {
        private readonly IProductRepository _productRepository;

        public GetProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Product GetProductById(Guid id) 
        {
            return _productRepository.GetProductById(id);
        }
        public IEnumerable <Product> GetAllProduct()
        {
            return _productRepository.GetAllProduct();
        }

        public IEnumerable<Product> GetProducts(string name = null, decimal? price = null) 
        {
            return _productRepository.Get(name);
        }

    }
}