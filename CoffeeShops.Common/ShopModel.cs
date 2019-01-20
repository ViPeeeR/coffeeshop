using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeShops.Common
{
    public class ShopModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public IEnumerable<ProductModel> Products { get; set; }
    }
}
