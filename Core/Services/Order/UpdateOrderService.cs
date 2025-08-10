using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using BusinessEntities;
using Common;
using Raven.Client.Linq.Indexing;

namespace Core.Services.Users
{
    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class UpdateOrderService : IUpdateOrderService
    {

        public void Update(Order order, DateTime OrderDate, List<Product> Products)
        {
            order.SetOrderDate(OrderDate);
            order.SetProduct(Products);
        }
    }
}