using CoffeeShops.Shops.API.Abstracts;
using CoffeeShops.Shops.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShops.Shops.API.Repository
{
    public class ProductRepository : IProductRepository
    {
        public Task Add(Product item)
        {
            throw new NotImplementedException();
        }

        public Task<Product> Get(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task Remove(string id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Product item)
        {
            throw new NotImplementedException();
        }
    }
}
