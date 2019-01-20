using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShops.Orders.API.Models
{
    public class Order
    {
        [Key]
        public string Id { get; set; }

        public string ClientId { get; set; }

        public string ShopId { get; set; }

        public IEnumerable<ProductItem> Products { get; set; }

        public DateTime Date { get; set; }

        public DateTime DateDelivery { get; set; }

        public string Comment { get; set; }

        public StatusPayment StatusPayment { get; set; }

        public StatusOrder StatusOrder { get; set; }
    }

    public enum StatusPayment
    {
        Wait = 0,
        Paid,
        Cancel
    }

    public enum StatusOrder
    {
        WaitPayment = 0,
        Prepares,
        Ready
    }
}
