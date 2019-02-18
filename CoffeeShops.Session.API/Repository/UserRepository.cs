using CoffeeShops.Session.API.Abstracts;
using CoffeeShops.Session.API.Context;
using CoffeeShops.Session.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShops.Session.API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<User> Add(User item)
        {
            var result = await _context.Users.AddAsync(item);
            await _context.SaveChangesAsync();
            return await Get(result.Entity.Id);
        }

        public async Task<User> Get(string id)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return user;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var users = await _context.Users
                .AsNoTracking()
                .ToListAsync();

            return users;
        }

        public async Task<User> GetByLogin(string login)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Login == login);

            return user;
        }

        public async Task Remove(string id)
        {
            var user = await Get(id);
            if (user == null)
                throw new Exception("User not found!");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task Update(User item)
        {
            var user = Get(item.Id);
            if (user == null)
            {
                await Add(item);
            }
            else
            {
                _context.Users.Update(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
