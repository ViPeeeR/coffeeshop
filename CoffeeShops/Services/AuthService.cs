using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CoffeeShops.Common;
using CoffeeShops.Infrustructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CoffeeShops.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<AuthService> _logger;
        private readonly UrlHosts _urls;

        public AuthService(HttpClient httpClient, ILogger<AuthService> logger, IOptions<UrlHosts> config)
        {
            _httpClient = httpClient;
            _logger = logger;
            _urls = config.Value;
        }

        public async Task<AuthenticateModel> Login(LoginModel model)
        {
            var data = await _httpClient.PostAsJsonAsync(_urls.Session + $"/api/v1/session/login", model);
            if (data.IsSuccessStatusCode)
            {
                return await data.Content.ReadAsAsync(typeof(AuthenticateModel)) as AuthenticateModel;
            }
            return null;
        }

        public async Task<AuthenticateModel> Register(RegisterModel model)
        {
            var data = await _httpClient.PostAsJsonAsync(_urls.Session + $"/api/v1/session/register", model);
            if (data.IsSuccessStatusCode)
            {
                return await data.Content.ReadAsAsync(typeof(AuthenticateModel)) as AuthenticateModel;
            }
            return null;
        }

        public async Task<string> AuthCode(string clientId)
        {
            var data = await _httpClient.GetStringAsync(_urls.Session + $"/api/v1/session/code?clientId={clientId}");
            return data;
        }

        public async Task<AuthenticateModel> Login(string clientId, string clientSecret, string code)
        {
            var data = await _httpClient.PostAsJsonAsync(_urls.Session + $"/api/v1/session/OAuth?clientId={clientId}&clientSecret={clientSecret}&code={code}", new object());
            if (data.IsSuccessStatusCode)
            {
                return await data.Content.ReadAsAsync(typeof(AuthenticateModel)) as AuthenticateModel;
            }
            return null;
        }

        public async Task<AuthenticateModel> Refresh(AuthenticateModel model)
        {
            var data = await _httpClient.PostAsJsonAsync(_urls.Session + $"/api/v1/session/refresh", model);
            if (data.IsSuccessStatusCode)
            {
                return await data.Content.ReadAsAsync(typeof(AuthenticateModel)) as AuthenticateModel;
            }
            return null;
        }

        public async Task<bool> Validate(HttpRequest request)
        {
            var header = request.Headers["Authorization"];
            var token = header[0]?.Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token))
                return false;

            return await Validate(token);
        }

        public async Task<bool> Validate(AuthenticateModel model)
        {
            var data = await _httpClient.PostAsJsonAsync(_urls.Session + $"/api/v1/session/verify", model);
            if (data.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task<bool> Validate(string token)
        {
            var model = new AuthenticateModel() { AccessToken = token };
            return await Validate(model);
        }
    }
}
