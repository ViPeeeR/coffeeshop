using Newtonsoft.Json;
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
            var obj = JsonConvert.DeserializeObject<RefreshToken>(serialized);
            Value = obj.Value;
            Expiration = obj.Expiration;
        }

        public string Value { get; set; }

        public int Expiration { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
