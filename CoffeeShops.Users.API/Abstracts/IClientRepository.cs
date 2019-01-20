using CoffeeShops.Abstract;
using CoffeeShops.Users.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShops.Users.API.Abstracts
{
    public interface IClientRepository : IRepository<Client>
    {
    }
}
