using CoffeeShops.Common;
using CoffeeShops.Infrustructure;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoffeeShops.Services
{
    public class AuthServicesService : IAuthServicesService
    {
        private readonly HttpClient _httpClient;
        private readonly ServerToken _serverToken;
        private readonly UrlHosts _urls;

        public AuthServicesService(HttpClient httpClient, ServerToken serverToken, IOptions<UrlHosts> config)
        {
            _httpClient = httpClient;
            _serverToken = serverToken;
            _urls = config.Value;
        }

        public async Task AuthClient()
        {
            var data = await _httpClient.GetStringAsync(_urls.Client + $"/api/v1/auth/login?appId={"user_api"}&appSecret={"key_user_api"}");
            var token = !string.IsNullOrEmpty(data)
               ? JsonConvert.DeserializeObject<AuthServiceToken>(data)
               : null;
            
            if (token != null)
            {
                _serverToken.Client = token;
            }
        }

        public async Task AuthOrder()
        {
            var data = await _httpClient.GetStringAsync(_urls.Order + $"/api/v1/auth/login?appId={"order_api"}&appSecret={"key_order_api"}");
            var token = !string.IsNullOrEmpty(data)
               ? JsonConvert.DeserializeObject<AuthServiceToken>(data)
               : null;

            if (token != null)
            {
                _serverToken.Order = token;
            }
        }

        public async Task AuthShop()
        {
            var data = await _httpClient.GetStringAsync(_urls.Shop + $"/api/v1/auth/login?appId={"shop_api"}&appSecret={"key_shop_api"}");
            var token = !string.IsNullOrEmpty(data)
               ? JsonConvert.DeserializeObject<AuthServiceToken>(data)
               : null;

            if (token != null)
            {
                _serverToken.Shop = token;
            }
        }
    }
}
