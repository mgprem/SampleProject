
using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Data.Repositories
{

    public static class InMemoryDatabase
    {
        public static List<Product> Products { get; set; }
        public static List<Order> Orders { get; set; }

        static InMemoryDatabase()
        {
            Products = new List<Product>();
            Orders = new List<Order>();
        }
    }
    
}