using BusinessEntities;
//using ProductOrderApi;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Web.Helpers;
using System.Web.UI;

namespace WebApi.Models.Users
{
    public class OrderData : IdObjectData
    {
        public OrderData(Order order) : base(order)
        {
            OrderDate = order.OrderDate;
            Products = new List<Product>(order.Products); 
        }

        public DateTime OrderDate { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}