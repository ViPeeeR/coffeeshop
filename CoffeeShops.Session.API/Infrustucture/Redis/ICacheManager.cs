using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShops.Session.API.Infrustucture.Redis
{
    public interface ICacheManager
    {
        Task SetCache(string key, string value);

        Task<string> GetCache(string key);

        Task Remove(string key);
    }
}
