using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CoffeeShops.Common;
using CoffeeShops.Infrustructure;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CoffeeShops.Services
{
    public class ShopService : IShopService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ShopService> _logger;
        private readonly UrlHosts _urls;

        public ShopService(HttpClient httpClient, ILogger<ShopService> logger, IOptions<UrlHosts> config)
        {
            _httpClient = httpClient;
            _logger = logger;
            _urls = config.Value;
        }

        public async Task Create(ShopModel model)
        {
            await _httpClient.PostAsJsonAsync(_urls.Client + "/api/v1/shop", model);
        }

        public async Task<IEnumerable<ShopModel>> GetAll(int page, int size)
        {
            var data = await _httpClient.GetStringAsync(_urls.Client + $"/api/v1/shop?page={page}&size={size}");
            var shops = !string.IsNullOrEmpty(data)
                ? JsonConvert.DeserializeObject<IEnumerable<ShopModel>>(data)
                : null;
            return shops;
        }

        public async Task<ShopModel> GetById(string id)
        {
            var data = await _httpClient.GetStringAsync(_urls.Client + $"/api/v1/shop/{id}");
            var shop = !string.IsNullOrEmpty(data) ? JsonConvert.DeserializeObject<ShopModel>(data) : null;
            return shop;
        }

        public async Task Remove(string id)
        {
            await _httpClient.DeleteAsync(_urls.Client + $"/api/v1/shop/{id}");
        }

        public async Task Update(ShopModel model)
        {
            await _httpClient.PutAsJsonAsync(_urls.Client + $"/api/v1/shop", model);
        }
    }
}
