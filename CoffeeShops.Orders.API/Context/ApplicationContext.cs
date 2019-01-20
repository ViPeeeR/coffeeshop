using CoffeeShops.Orders.API.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShops.Orders.API.Context
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        public DbSet<ProductItem> Products { get; set; }

        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }
    }
}
