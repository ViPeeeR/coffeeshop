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
    }
}
