using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    [RoutePrefix("orders")]
    public class OrderController : ApiController
    {
        private static readonly Dictionary<Guid, OrderModel> _orders = new Dictionary<Guid, OrderModel>();

        [HttpPost]
        [Route("{id:guid}/create")]
        public HttpResponseMessage Create(Guid id, [FromBody] OrderModel model)
        {
            if (_orders.ContainsKey(id))
            {
                return Request.CreateResponse(HttpStatusCode.Conflict, "Record already exists.");
            }
                model.Id = id;
                _orders[id] = model;

            return Request.CreateResponse(HttpStatusCode.OK, model);
        }

        [HttpPost]
        [Route("{id:guid}/update")]
        public HttpResponseMessage Update(Guid id, [FromBody] OrderModel model)
        {

                if (!_orders.ContainsKey(id))
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Order not found.");

                model.Id = id;
                _orders[id] = model;

            return Request.CreateResponse(HttpStatusCode.OK, model);
        }

        [HttpDelete]
        [Route("{id:guid}/delete")]
        public HttpResponseMessage Delete(Guid id)
        {

            if (_orders.ContainsKey(id))
            {
                _orders.Remove(id);
                return Request.CreateResponse(HttpStatusCode.OK, "Deleted successfully.");
            }


            return Request.CreateResponse(HttpStatusCode.NotFound, "Order not found.");
        }

        [HttpGet]
        [Route("{id:guid}")]
        public HttpResponseMessage GetById(Guid id)
        {
            OrderModel order;

                _orders.TryGetValue(id, out order);


            if (order != null)
                return Request.CreateResponse(HttpStatusCode.OK, order);

            return Request.CreateResponse(HttpStatusCode.NotFound, "Order not found.");
        }

        [HttpGet]
        [Route("list")]
        public HttpResponseMessage GetAll()
        {
            List<OrderModel> orderList;

                orderList = _orders.Values.ToList();

            return Request.CreateResponse(HttpStatusCode.OK, orderList);
        }
    }

    public class OrderModel
    {
        public Guid Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}