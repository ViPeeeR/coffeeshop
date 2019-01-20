using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeShops.Common
{
    public class ProductModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string ShopId { get; set; }
    }
}
