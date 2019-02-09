using CoffeeShops.Abstract;
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
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationContext _context;

        public ProductRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Product> Add(Product item)
        {
            var result = await _context.Products.AddAsync(item);
            await _context.SaveChangesAsync();
            return await Get(result.Entity.Id);
        }

        public async Task<Product> Get(string id)
        {
            var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return product;
        }

        public async Task<IEnumerable<Product>> GetAll(string shopId)
        {
            var products = await _context.Products.AsNoTracking().Where(x => x.ShopId == shopId).ToListAsync();
            return products;
        }

        public async Task Remove(string id)
        {
            var product = await Get(id);
            if (product == null)
                throw new Exception("Client not found!");

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Product item)
        {
            var product = Get(item.Id);
            if (product == null)
            {
                await Add(item);
            }
            else
            {
                _context.Products.Update(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
