using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShops.Common
{
    public class AuthenticateModel
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
