using CoffeeShops.Users.API.Abstracts;
using CoffeeShops.Users.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShops.Users.API.Repository
{
    public class ClientRepository : IClientRepository
    {
        public Task Add(Client item)
        {
            throw new NotImplementedException();
        }

        public Task<Client> Get(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Client>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task Remove(string id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Client item)
        {
            throw new NotImplementedException();
        }
    }
}
