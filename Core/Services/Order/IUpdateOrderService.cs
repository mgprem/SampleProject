using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Core.Services.Users
{
    public interface IUpdateOrderService
    {
        void Update(Order order, DateTime OrderDate, List<Product> Products);
    }
}