using System;
using System.Collections.Generic;
using BusinessEntities;
using Common;
using Core.Factories;
using Data.Repositories;

namespace Core.Services.Users
{
    [AutoRegister]
    public class CreateProductService : ICreateProductService
    {
        private readonly IUpdateProductService _updateProductService;
        private readonly IIdObjectFactory<Product> _productFactory;
        private readonly IProductRepository _productRepository;

        public CreateProductService(IIdObjectFactory<Product> productFactory, IProductRepository productRepository, IUpdateProductService updateProductService)
        {
            _productFactory = productFactory;
            _productRepository = productRepository;
            _updateProductService = updateProductService;
        }

        public Product Create(Guid id, string name, decimal? price)
        {
            var product = _productFactory.Create(id);
            _updateProductService.Update(product, name, price);
            _productRepository.Save(product);
            return product;
        }

        //public Product Create(Guid id, string name, string price)
        //{
        //    var Product = _productFactory.Create(id);
        //    _updateUserService.Update(user, name, email, type, age, annualSalary, tags);
        //    _userRepository.Save(user);
        //    return user;
        //}

        //public Product Create(Guid id, string name, decimal? price)
        //{
        //    var Product = _productFactory.Create(id);
        //    _updateUserService.Update(user, name);
        //    _userRepository.Save(user);
        //    return user;
        //}
    }
}