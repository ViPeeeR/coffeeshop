using CoffeeShops.Users.API.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShops.Users.API.Context
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }


        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }
    }
}
