using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShops.Session.API.Infrustucture.Redis
{
    public class CacheManager : ICacheManager
    {
        private readonly IDistributedCache _distributedCache;

        public CacheManager(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<string> GetCache(string key)
        {
            return await _distributedCache.GetStringAsync(key);
        }

        public async Task SetCache(string key, string value)
        {
            await _distributedCache.SetStringAsync(key, value);
        }

        public async Task Remove(string key)
        {
            await _distributedCache.RemoveAsync(key);
        }
    }
}
