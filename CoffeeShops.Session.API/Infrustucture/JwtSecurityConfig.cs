using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShops.Session.API.Infrastructure
{
    public class JwtSecurityConfig
    {
        public string SigningKey { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public int LifeSpan { get; set; }
    }
}
