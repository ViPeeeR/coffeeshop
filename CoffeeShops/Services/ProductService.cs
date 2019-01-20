using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CoffeeShops.Common;
using CoffeeShops.Infrustructure;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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

        public Task Create(ProductModel model)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductModel>> GetAll(string shopId, int page, int size)
        {
            throw new NotImplementedException();
        }

        public Task Remove(string id)
        {
            throw new NotImplementedException();
        }

        public Task Update(ProductModel model)
        {
            throw new NotImplementedException();
        }
    }
}
