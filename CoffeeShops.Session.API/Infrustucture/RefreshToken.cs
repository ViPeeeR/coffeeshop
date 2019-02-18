using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShops.Session.API.Infrustucture
{
    public class RefreshToken
    {
        public RefreshToken()
        {
        }

        public RefreshToken(string serialized)
        {
            var values = serialized.Split(';');
            Value = values[0];
            Expiration = int.Parse(values[1]);
        }

        public string Value { get; set; }

        public int Expiration { get; set; }

        public override string ToString()
        {
            return $"{Value};{Expiration}";
        }
    }
}
