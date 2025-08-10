using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Core.Services.Users
{
    public interface IGetOrderService
    {
        IEnumerable<Order> GetAllOrders();
        Order GetOrderById(Guid id);

        IEnumerable<Order> GetOrders(DateTime? OrderDate, List<Product> Products);
    }
}