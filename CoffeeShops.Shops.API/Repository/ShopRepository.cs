using CoffeeShops.Shops.API.Abstracts;
using CoffeeShops.Shops.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShops.Shops.API.Repository
{
    public class ShopRepository : IShopRepository
    {
        public Task Add(Shop item)
        {
            throw new NotImplementedException();
        }

        public Task<Shop> Get(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Shop>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task Remove(string id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Shop item)
        {
            throw new NotImplementedException();
        }
    }
}
