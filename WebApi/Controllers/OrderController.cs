using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using BusinessEntities;
using DomainProduct = BusinessEntities.Product;
//using BusinessEntities;
using Core.Services.Users;
using ProductOrderApi;
using WebApi.Models.Users;
using Core.Factories;
//using BusinessEntities;
//using BusinessEntities;

namespace WebApi.Controllers
{
    [RoutePrefix("orders")]
    public class OrderController : BaseApiController
    {
        private readonly ICreateOrderService _createOrderService;
        private readonly IDeleteOrderService _deleteOrderService;
        private readonly IGetOrderService _getOrderService;
        private readonly IUpdateOrderService _updateOrderService;

        public OrderController(ICreateOrderService createOrderService, IDeleteOrderService deleteOrderService, IGetOrderService getOrderService, IUpdateOrderService updateOrderService
            )
        {
            _createOrderService = createOrderService;
            _deleteOrderService = deleteOrderService;
            _getOrderService = getOrderService;
            _updateOrderService = updateOrderService;
        }

        [Route("{OrderId:guid}/create")]
        [HttpPost]
        public HttpResponseMessage CreateOrder(Guid OrderId, [FromBody] Order model)
        {
            var Orderid = _getOrderService.GetOrderById(OrderId);
            if (Orderid != null)
            {
                return AlreadyExists("Record Already Exists");
            }

            // Map API products to domain products
            var domainProducts = MapToDomainProducts(model.Products);

            var Order = _createOrderService.Create(OrderId, model.OrderDate, domainProducts);
            return Found(new OrderData(Order));
        }


       
        [Route("{OrderId:guid}/update")]
        [HttpPut]
        public HttpResponseMessage UpdateOrder(Guid OrderId, [FromBody] Order model)
        {
            if (model == null)
                return RequestBad("Order payload is required.");

            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

            var Order = _getOrderService.GetOrderById(OrderId);
            if (Order == null)
            {
                return DoesNotExist();
            }
            var domainProducts = MapProductsForUpdate(model.Products, Order.Products);
            _updateOrderService.Update(Order, model.OrderDate, domainProducts);
            return Found(new OrderData(Order));
        }

     
                [Route("{OrderId:guid}/delete")]
                [HttpDelete]
                public HttpResponseMessage DeleteOrder(Guid OrderId)
                {
                    var Order = _getOrderService.GetOrderById(OrderId);
                    if (Order == null)
                    {
                        return DoesNotExist();
                    }
                    _deleteOrderService.Delete(Order);
                    return Found();
                }
   
                [Route("{OrderId:guid}")]
                [HttpGet]
                public HttpResponseMessage GetOrder(Guid OrderId)
                {
                    var Order = _getOrderService.GetOrderById(OrderId);
                    if (Order == null)
                    {
                        return DoesNotExist();
                    }
                    return Found(new OrderData(Order));
                }
        [Route("AllOrders")]
        [HttpGet]
        public HttpResponseMessage GetAllOrders()
        {
            var Orders = _getOrderService.GetAllOrders()
                                       .Select(q => new OrderData(q))
                                       .ToList();

            return Found(Orders);
        }

        [Route("list")]
        [HttpGet]
        public HttpResponseMessage GetOrders(int skip, int take, [FromBody] Order model, DateTime? orderDate = null)
        {
            //if ((orderDate.HasValue) && (model != null))
            //{

            var domainProducts = new List<DomainProduct>();

            if (model != null && model.Products != null && model.Products.Count > 0)
            {
                if (model.Id != Guid.Empty)
                {
                    var Order = _getOrderService.GetOrderById(model.Id);
                    if (Order == null)
                    {
                        return DoesNotExist();
                    }

                    domainProducts = MapProductsForUpdate(model.Products, Order?.Products);
                }
            }
            

                var Orders = _getOrderService.GetOrders(orderDate, domainProducts)
                                         .Skip(skip).Take(take)
                                         .Select(q => new OrderData(q))
                                         .ToList();

                return Found(Orders);
            //}
        }

        [Route("clear")]
        [HttpDelete]
        public HttpResponseMessage DeleteAllOrders()
        {
            _deleteOrderService.DeleteAll();
            return Found();
        }
        private static List<DomainProduct> MapToDomainProducts(List<ProductOrderApi.Product> apiProducts)
        {
            var result = new List<DomainProduct>();
            if (apiProducts == null) return result;

            foreach (var p in apiProducts)
            {
                var dp = new DomainProduct();
                dp.SetName(p.Name);
                dp.SetPrice(p.Price);
                result.Add(dp);

            }
            return result;
        }
        private List<DomainProduct> MapProductsForUpdate(IList<ProductOrderApi.Product> incoming, IEnumerable<DomainProduct> existing)
        {
            var results = new List<DomainProduct>();
            var existingById = (existing ?? new List<DomainProduct>()).ToDictionary(p => p.Id);

            foreach (var p in incoming ?? new List<ProductOrderApi.Product>())
            {
                if (p == null) continue;

                // If the product already exists in the order, update IN PLACE
                DomainProduct target;
                if (p.Id != Guid.Empty && existingById.TryGetValue(p.Id, out target))
                {
                    target.SetName(p.Name);
                    target.SetPrice(p.Price);
                    results.Add(target);
                }
            }

            return results;
        }

    }
}