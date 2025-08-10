using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using BusinessEntities;
using Common;
using Data.Indexes;
using Raven.Client;
using Raven.Client.Document;

namespace Data.Repositories
{
    [AutoRegister]
    public class OrderRepository : OrderBaseRepository<Order>, IOrderRepository
    {
        public OrderRepository() : base(InMemoryDatabase.Orders, o => o.Id)
        {

        }

        public void DeleteAll()
        {
            DeleteAllInMemory();
        }
        public IEnumerable<Order> GetAllOrders()
        {
            
            return GetAllInMemory();
        }

        public Order GetProductById(Guid id)
        {
            
            return base.Get(id);

        }

        public IEnumerable<Order> GetOrders(DateTime? OrderDate, IEnumerable<Product> Products=null)
        {
            /*
            No Filters : if (OrderDate = default, Products = null) then All Orders are returned
            Filter by Date : if (OrderDate ='2023-01-01', Products = null) then Orders with matching OrderDate are returned
            Filter by Products : if (OrderDate = default, Products = [Product1, Product2]) then Orders containing Product1 or Product2 are returned
            Filter by Date and Products : if (OrderDate ='2023-01-01', Products = [Product1, Product2]) then Orders with matching OrderDate and containing Product1 or Product2 are returned
            */

            var items = GetAllInMemory();
            var filterByDate = OrderDate.HasValue && OrderDate != default(DateTime);
            var productIds = new HashSet<Guid>(
                                                (Products ?? Enumerable.Empty<Product>())
                                                    .Where(p => p != null && p.Id != Guid.Empty)
                                                    .Select(p => p.Id)
                                            );
            var filterByProducts = productIds.Count > 0;

            return items.Where(o =>
                (!filterByDate || o.OrderDate.Date == OrderDate?.Date) &&
                (!filterByProducts || (o.Products != null && o.Products.Any(op => productIds.Contains(op.Id))))
            );
        }

        public Order GetOrderById(Guid id)
        {
            return base.Get(id);
        }

    }
}