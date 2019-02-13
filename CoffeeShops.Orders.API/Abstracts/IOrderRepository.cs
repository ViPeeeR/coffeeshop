using CoffeeShops.Abstract;
using CoffeeShops.Orders.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShops.Orders.API.Abstracts
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetAll();

        Task<Order> AddProducts(IEnumerable<ProductItem> productItems);

        Task<IEnumerable<Order>> GetByClientId(string id);

        Task<IEnumerable<Order>> GetByShopId(string id);
    }
}
