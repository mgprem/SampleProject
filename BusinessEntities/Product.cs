using System;
using System.Collections.Generic;
using Common.Extensions;

namespace BusinessEntities
{
    public class Product : IdObject
    {
        private string _name;
        private decimal? _price;

        public string Name
        {
            get => _name;
            private set => _name = value;
        }

        public decimal? Price
        {
            get => _price;
            private set => _price = value;
        }
        public void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("Name was not provided.");
            }
            _name = name;
        }

        public void SetPrice(decimal? price)
        {
            _price = price;
        }

    }
}