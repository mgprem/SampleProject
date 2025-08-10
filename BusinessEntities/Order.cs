using System;
using System.Collections.Generic;
using Common.Extensions;

namespace BusinessEntities
{
    public class Order : IdObject
    {
        private DateTime _orderdate;
        private List<Product> _products;

        public DateTime OrderDate
        {
            get => _orderdate;
            private set => _orderdate = value;
        }

        public List<Product> Products
        {
            get => _products;
            private set => _products = value;
        }
        public void SetOrderDate(DateTime orderdate)
        {
            if (orderdate == default(DateTime) || orderdate == DateTime.MinValue)
            {
                throw new ArgumentNullException("OrderDate was not provided.");
            }
            _orderdate = orderdate;
        }

        public void SetProduct(List<Product> products)
        {
            if (products == null)
                throw new ArgumentNullException("products", "Products list cannot be null.");

            _products = products;
        }

    }
}