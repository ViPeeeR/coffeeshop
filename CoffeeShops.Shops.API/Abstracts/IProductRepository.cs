using CoffeeShops.Abstract;
using CoffeeShops.Shops.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShops.Shops.API.Abstracts
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetAll(string shopId);

        Task AddRange(IEnumerable<Product> products);

    }
}
