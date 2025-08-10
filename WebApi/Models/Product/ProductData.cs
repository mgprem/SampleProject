using BusinessEntities;
using System.Collections.Generic;
using System.Diagnostics.Tracing;

namespace WebApi.Models.Users
{
    public class ProductData : IdObjectData
    {
        public ProductData(Product product) : base(product)
        {
            Name = product.Name;
            Price = product.Price;
        }

        public string Name { get; set; }
        public decimal? Price { get; set; }
    }
}