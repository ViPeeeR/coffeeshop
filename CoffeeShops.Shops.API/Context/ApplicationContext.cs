using CoffeeShops.Shops.API.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShops.Shops.API.Context
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Shop> Shops { get; set; }

        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }
    }
}
