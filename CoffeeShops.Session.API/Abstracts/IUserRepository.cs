using CoffeeShops.Abstract;
using CoffeeShops.Session.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShops.Session.API.Abstracts
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByLogin(string login);
    }
}
