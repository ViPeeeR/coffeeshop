using CoffeeShops.Shops.API.Abstracts;
using CoffeeShops.Shops.API.Context;
using CoffeeShops.Shops.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShops.Shops.API.Repository
{
    public class ShopRepository : IShopRepository
    {
        private readonly ApplicationContext _context;

        public ShopRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Shop> Add(Shop item)
        {
            var result = await _context.Shops.AddAsync(item);
            await _context.SaveChangesAsync();
            return await Get(result.Entity.Id);
        }

        public async Task<Shop> Get(string id)
        {
            var shop = await _context.Shops
                .Include(x => x.Products)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return shop;
        }

        public async Task<IEnumerable<Shop>> GetAll()
        {
            var shops = await _context.Shops
                .Include(x => x.Products)
                .AsNoTracking()
                .ToListAsync();

            return shops;
        }

        public async Task Remove(string id)
        {
            var shop = await Get(id);
            if (shop == null)
                throw new Exception("Client not found!");

            var products = _context.Products.Where(x => x.ShopId == id);

            _context.Shops.Remove(shop);
            _context.Products.RemoveRange(products);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Shop item)
        {
            var shop = Get(item.Id);
            if (shop == null)
            {
                await Add(item);
            }
            else
            {
                _context.Shops.Update(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
