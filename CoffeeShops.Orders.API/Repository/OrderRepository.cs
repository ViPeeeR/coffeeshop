using CoffeeShops.Orders.API.Abstracts;
using CoffeeShops.Orders.API.Context;
using CoffeeShops.Orders.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShops.Orders.API.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationContext _context;

        public OrderRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Order> Add(Order item)
        {
            var result = await _context.Orders.AddAsync(item);
            await _context.SaveChangesAsync();
            return await Get(result.Entity.Id);
        }

        public async Task<Order> AddProducts(IEnumerable<ProductItem> productItems)
        {
            await _context.Products.AddRangeAsync(productItems);
            await _context.SaveChangesAsync();

            return await Get(productItems.First().Id);
        }

        public async Task<Order> Get(string id)
        {
            var order = await _context.Orders.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == id);
            return order;
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            var orders = await _context.Orders.Include(x => x.Products).ToListAsync();
            return orders;
        }

        public async Task<IEnumerable<Order>> GetByClientId(string id)
        {
            var orders = await _context.Orders.Include(x => x.Products)
                .Where(x => x.ClientId == id)
                .AsNoTracking().ToListAsync();
            return orders;
        }

        public async Task<IEnumerable<Order>> GetByShopId(string id)
        {
            var orders = await _context.Orders.Include(x => x.Products)
               .Where(x => x.ShopId == id)
               .AsNoTracking().ToListAsync();
            return orders;
        }

        public async Task<Order> Remove(string id)
        {
            var order = await Get(id);
            if (order == null)
                throw new Exception("Order not found!");

            _context.Orders.Remove(order);
            _context.Products.RemoveRange(order.Products);
            await _context.SaveChangesAsync();

            return order;
        }

        public async Task RemoveByClientId(string clientId)
        {
            var orders = await GetByClientId(clientId);
            _context.Orders.RemoveRange(orders);
            foreach (var item in orders)
            {
                _context.Products.RemoveRange(item.Products);
            }
            await _context.SaveChangesAsync();
        }

        public async Task Update(Order item)
        {
            var order = Get(item.Id);
            if (order == null)
            {
                await Add(item);
            }
            else
            {
                _context.Orders.Update(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
