using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using BusinessEntities;
using Core.Services.Users;
using ProductOrderApi;
using WebApi.Models.Users;

namespace WebApi.Controllers
{
    [RoutePrefix("products")]
    public class ProductController : BaseApiController
    {
        private readonly ICreateProductService _createProductService;
        private readonly IDeleteProductService _deleteProductService;
        private readonly IGetProductService _getProductService;
        private readonly IUpdateProductService _updateProductService;

        public ProductController(ICreateProductService createProductService, IDeleteProductService deleteProductService, IGetProductService getProductService, IUpdateProductService updateProductService)
        {
            _createProductService = createProductService;
            _deleteProductService = deleteProductService;
            _getProductService = getProductService;
            _updateProductService = updateProductService;
        }

        [Route("{productId:guid}/create")]
        [HttpPost]
        public HttpResponseMessage CreateProduct(Guid ProductId, [FromBody] Product model)
        {
            var Productid = _getProductService.GetProductById(ProductId);
            if(Productid != null)
            {
                return AlreadyExists("Record Already Exists"); 
            }
            var product = _createProductService.Create(ProductId, model.Name, model.Price);
            return Found(new ProductData(product));
        }

        [Route("{ProductId:guid}/update")]
        [HttpPut]
        public HttpResponseMessage UpdateProduct(Guid ProductId, [FromBody] Product model)
        {
            if (!ModelState.IsValid)
            {
                var response = Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                return response;
            }

            var Product = _getProductService.GetProductById(ProductId);
            if (Product == null)
            {
                return DoesNotExist();
            }
            _updateProductService.Update(Product, model.Name, model.Price);
            return Found(new ProductData(Product));
        }

        [Route("{ProductId:guid}/delete")]
        [HttpDelete]
        public HttpResponseMessage DeleteProduct(Guid ProductId)
        {
            var Product = _getProductService.GetProductById(ProductId);
            if (Product == null)
            {
                return DoesNotExist();
            }
            _deleteProductService.Delete(Product);
            return Found();
        }

        [Route("{ProductId:guid}")]
        [HttpGet]
        public HttpResponseMessage GetProduct(Guid ProductId)
        {
            var Product = _getProductService.GetProductById(ProductId);
            if (Product == null)
            {
                return DoesNotExist();
            }
            return Found(new ProductData(Product));
        }
        [Route("AllProducts")]
        [HttpGet]
        public HttpResponseMessage GetAllProducts()
        {
            var Products = _getProductService.GetAllProduct()
                                       .Select(q => new ProductData(q))
                                       .ToList();

            return Found(Products);
        }

        [Route("list")]
        [HttpGet]
        public HttpResponseMessage GetProducts(int skip, int take,  string name = null, decimal? price = null)
        {
            var Products = _getProductService.GetProducts( name, price)
                                       .Skip(skip).Take(take)
                                       .Select(q => new ProductData(q))
                                       .ToList();
            return Found(Products);
        }

        [Route("clear")]
        [HttpDelete]
        public HttpResponseMessage DeleteAllProducts()
        {
            _deleteProductService.DeleteAll();
            return Found();
        }

    }
}