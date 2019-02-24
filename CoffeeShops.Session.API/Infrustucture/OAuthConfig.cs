using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShops.Session.API.Infrustucture
{
    public class OAuthConfig
    {
        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public int LifeSpan { get; set; }
    }
}
