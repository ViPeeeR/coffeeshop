using CoffeeShops.Orders.API.Abstracts;
using CoffeeShops.Orders.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShops.Orders.API.Repository
{
    public class OrderRepository : IOrderRepository
    {
        public Task Add(Order item)
        {
            throw new NotImplementedException();
        }

        public Task<Order> Get(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task Remove(string id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Order item)
        {
            throw new NotImplementedException();
        }
    }
}
