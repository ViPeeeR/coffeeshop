using CoffeeShops.Users.API.Abstracts;
using CoffeeShops.Users.API.Context;
using CoffeeShops.Users.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShops.Users.API.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationContext _context;

        public ClientRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Client> Add(Client item)
        {
            var result = await _context.Clients.AddAsync(item);
            await _context.SaveChangesAsync();
            return await Get(result.Entity.Id);
        }

        public async Task<Client> Get(string id)
        {
            var client = await _context.Clients.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return client;
        }

        public async Task<IEnumerable<Client>> GetAll()
        {
            var clients = await _context.Clients.AsNoTracking().ToListAsync();
            return clients;
        }

        public async Task<Client> Remove(string id)
        {
            var client = await Get(id);
            if (client == null)
                throw new Exception("Client not found!");

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return client;
        }

        public async Task Update(Client item)
        {
            var client = Get(item.Id);
            if (client == null)
            {
                await Add(item);
            }
            else
            {
                _context.Clients.Update(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
