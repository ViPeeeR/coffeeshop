using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShops.Orders.API.Models
{
    public class ProductItem
    {
        [Key]
        public string Id { get; set; }

        public string ProductId { get; set; }

        public int Count { get; set; }

        public string OrderId { get; set; }

        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; }
    }
}
