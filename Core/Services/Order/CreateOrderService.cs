using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Linq;
using BusinessEntities;
using Common;
using Core.Factories;
using Data.Repositories;

namespace Core.Services.Users
{
    [AutoRegister]
    public class CreateOrderService : ICreateOrderService
    {
        private readonly IUpdateOrderService _updateOrderService;
        private readonly IIdObjectFactory<Order> _orderFactory;
        private readonly IOrderRepository _orderRepository;

        public CreateOrderService(IIdObjectFactory<Order> OrderFactory, IOrderRepository OrderRepository, IUpdateOrderService updateOrderService)
        {
            _orderFactory = OrderFactory;
            _orderRepository = OrderRepository;
            _updateOrderService = updateOrderService;
        }

        public Order Create(Guid id, DateTime OrderDate, List<Product> Products)
        {
            var Order = _orderFactory.Create(id);
            _updateOrderService.Update(Order, OrderDate, Products);
            _orderRepository.Save(Order);
            return Order;
        }

    }
}