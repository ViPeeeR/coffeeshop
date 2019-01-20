using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShops.Shops.API.Models
{
    public class Shop
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }
            
        public IEnumerable<Product> Products { get; set; }
    }
}
