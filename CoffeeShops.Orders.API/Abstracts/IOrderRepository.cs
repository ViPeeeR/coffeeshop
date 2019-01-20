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
    }
}
