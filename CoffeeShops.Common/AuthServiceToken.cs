using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeShops.Common
{
    public class AuthServiceToken
    {
        public string Token { get; set; }

        public int Create { get; set; }

        public int Lifespan { get; set; }
    }
}
