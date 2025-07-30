using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    [RoutePrefix("products")]
    public class ProductController : ApiController
    {
        private static readonly Dictionary<Guid, ProductModel> _products = new Dictionary<Guid, ProductModel>();


        [HttpPost]
        [Route("{id:guid}/create")]
        public HttpResponseMessage Create(Guid id, [FromBody] ProductModel model)
        {

            if (_products.ContainsKey(id))
            {
                return Request.CreateResponse(HttpStatusCode.Conflict, "Record already exists.");
            }
            model.Id = id;
                _products[id] = model;
            return Request.CreateResponse(HttpStatusCode.OK, model);
        }

        [HttpPost]
        [Route("{id:guid}/update")]
        public HttpResponseMessage Update(Guid id, [FromBody] ProductModel model)
        {

                if (!_products.ContainsKey(id))
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Product not found.");

                model.Id = id;
                _products[id] = model;

            return Request.CreateResponse(HttpStatusCode.OK, model);
        }

        [HttpDelete]
        [Route("{id:guid}/delete")]
        public HttpResponseMessage Delete(Guid id)
        {
                if (_products.ContainsKey(id))
                {
                    _products.Remove(id);
                    return Request.CreateResponse(HttpStatusCode.OK, "Deleted successfully.");
                }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Product not found.");
        }

        [HttpGet]
        [Route("{id:guid}")]
        public HttpResponseMessage GetById(Guid id)
        {
            ProductModel product;

                _products.TryGetValue(id, out product);

            if (product != null)
                return Request.CreateResponse(HttpStatusCode.OK, product);

            return Request.CreateResponse(HttpStatusCode.NotFound, "Product not found.");
        }

        [HttpGet]
        [Route("list")]
        public HttpResponseMessage GetAll()
        {
            List<ProductModel> productList;

                productList = _products.Values.ToList();

            return Request.CreateResponse(HttpStatusCode.OK, productList);
        }
    }

    public class ProductModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}