using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Data.Repositories
{
    public interface IOrderRepository : IOrderBaseRepository<Order>
    {
        IEnumerable<Order> GetOrders(DateTime? OrderDate, IEnumerable<Product> Products);
        void DeleteAll();
        Order GetOrderById(Guid id);
        IEnumerable<Order> GetAllOrders();
    }
}