using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeShops.Common
{
    public class OrderModel
    {
        public string Id { get; set; }

        public string ClientId { get; set; }

        public ClientModel Client { get; set; }

        public string ShopId { get; set; }

        public IEnumerable<ProductItemModel> Products { get; set; }

        public DateTime Date { get; set; }

        public DateTime DateDelivery { get; set; }

        public string Comment { get; set; }

        public StatusPayment StatusPayment { get; set; }

        public StatusOrder StatusOrder { get; set; }
    }

    public class ProductItemModel
    {
        public string Id { get; set; }

        public string ProductId { get; set; }

        public int Count { get; set; }

        public string OrderId { get; set; }
    }
}
