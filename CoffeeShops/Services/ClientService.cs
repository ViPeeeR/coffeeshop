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
    public class ClientService : IClientService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ClientService> _logger;
        private readonly ServerToken _serverToken;
        private readonly IAuthServicesService _authServicesService;
        private readonly UrlHosts _urls;

        public ClientService(HttpClient httpClient, ILogger<ClientService> logger, IOptions<UrlHosts> config, ServerToken serverToken,
            IAuthServicesService authServicesService)
        {
            _httpClient = httpClient;
            _logger = logger;
            _serverToken = serverToken;
            _authServicesService = authServicesService;
            _urls = config.Value;
        }

        private async Task ValidateToken()
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            if (_serverToken.Client == null ||
                _serverToken.Client.Create + _serverToken.Client.Lifespan < (DateTime.UtcNow.ToUniversalTime() - origin).TotalSeconds)
            {
                await _authServicesService.AuthClient();
            }
            _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _serverToken.Client.Token);
        }

        public async Task Create(ClientModel model)
        {
            await ValidateToken();
            await _httpClient.PostAsJsonAsync(_urls.Client + "/api/v1/client", model);
        }

        public async Task<IEnumerable<ClientModel>> GetAll(int page, int size)
        {
            await ValidateToken();

            var data = await _httpClient.GetStringAsync(_urls.Client + $"/api/v1/client?page={page}&size={size}");
            var clients = !string.IsNullOrEmpty(data)
                ? JsonConvert.DeserializeObject<IEnumerable<ClientModel>>(data)
                : null;
            return clients;
        }

        public async Task<ClientModel> GetById(string id)
        {
            await ValidateToken();

            var data = await _httpClient.GetStringAsync(_urls.Client + $"/api/v1/client/{id}");
            var client = !string.IsNullOrEmpty(data) ? JsonConvert.DeserializeObject<ClientModel>(data) : null;
            return client;
        }

        public async Task Remove(string id)
        {
            await ValidateToken();

            await _httpClient.DeleteAsync(_urls.Client + $"/api/v1/client/{id}");
        }

        public async Task Update(ClientModel model)
        {
            await ValidateToken();

            await _httpClient.PutAsJsonAsync(_urls.Client + $"/api/v1/client", model);
        }
    }
}
