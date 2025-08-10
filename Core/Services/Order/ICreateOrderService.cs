using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Core.Services.Users
{
    public interface ICreateOrderService
    {
        Order Create(Guid id, DateTime OrderDate, List<Product> Products);
    }
}