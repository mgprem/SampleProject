using System;
using System.Collections.Generic;
using System.Linq;


namespace ProductOrderApi  
{
    public class Order
    {
        public Guid Id { get; set; } 
        public DateTime OrderDate { get; set; } 
        public List<Product> Products { get; set; } 
    }
}