using System;
using System.Collections.Generic;
using BusinessEntities;
using Common;
using Data.Repositories;

namespace Core.Services.Users
{
    [AutoRegister]
    public class GetOrderService : IGetOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public IEnumerable<Order> GetAllOrders()
        {
            return _orderRepository.GetAllOrders();
        }

        public Order GetOrderById(Guid id)
        {
            return _orderRepository.GetOrderById(id);
        }

        public IEnumerable<Order> GetOrders(DateTime? OrderDate, List<Product> Products=null)
        {
            return _orderRepository.GetOrders(OrderDate, Products);
        }
    }
}