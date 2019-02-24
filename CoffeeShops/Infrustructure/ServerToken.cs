using CoffeeShops.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShops.Infrustructure
{
    public class ServerToken
    {
        public AuthServiceToken Client { get; set; }

        public AuthServiceToken Order { get; set; }

        public AuthServiceToken Shop { get; set; }
    }
}
