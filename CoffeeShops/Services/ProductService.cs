using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CoffeeShops.Common;
using CoffeeShops.Infrustructure;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CoffeeShops.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ProductService> _logger;
        private readonly UrlHosts _urls;

        public ProductService(HttpClient httpClient, ILogger<ProductService> logger, IOptions<UrlHosts> config)
        {
            _httpClient = httpClient;
            _logger = logger;
            _urls = config.Value;
        }

        public async Task Create(ProductModel model)
        {
            await _httpClient.PostAsJsonAsync(_urls.Shop + "/api/v1/product", model);
        }

        public async Task<IEnumerable<ProductModel>> GetAll(string shopId, int page, int size)
        {
            var data = await _httpClient.GetStringAsync(_urls.Shop + $"/api/v1/product?shopId={shopId}&page={page}&size={size}");
            var products = !string.IsNullOrEmpty(data)
                ? JsonConvert.DeserializeObject<IEnumerable<ProductModel>>(data)
                : null;
            return products;
        }

        public async Task<ProductModel> GetById(string id)
        {
            var data = await _httpClient.GetStringAsync(_urls.Shop + $"/api/v1/product/{id}");
            var product = !string.IsNullOrEmpty(data) ? JsonConvert.DeserializeObject<ProductModel>(data) : null;
            return product;
        }

        public async Task Remove(string id)
        {
            await _httpClient.DeleteAsync(_urls.Shop + $"/api/v1/product/{id}");
        }

        public async Task Update(ProductModel model)
        {
            await _httpClient.PutAsJsonAsync(_urls.Shop + $"/api/v1/product", model);
        }
    }
}
