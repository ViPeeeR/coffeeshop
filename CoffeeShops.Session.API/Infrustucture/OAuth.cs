using CoffeeShops.Common;
using CoffeeShops.Session.API.Abstracts;
using CoffeeShops.Session.API.Infrastructure;
using CoffeeShops.Session.API.Infrustucture.Helpers;
using CoffeeShops.Session.API.Infrustucture.Redis;
using CoffeeShops.Session.API.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShops.Session.API.Infrustucture
{
    public class OAuth : IOAuth
    {
        private readonly IJwtAuth _jwtAuth;
        private readonly ICacheManager _cacheManager;
        private readonly ILogger<OAuth> _logger;
        private readonly IUserRepository _userRepository;
        private readonly OAuthConfig _oauthConfig;

        public OAuth(IJwtAuth jwtAuth,
            ICacheManager cacheManager,
            ILogger<OAuth> logger,
            IOptions<OAuthConfig> optionsOauth,
            IUserRepository userRepository)
        {
            _jwtAuth = jwtAuth;
            _cacheManager = cacheManager;
            _logger = logger;
            _userRepository = userRepository;
            _oauthConfig = optionsOauth.Value;
        }

        public async Task<AuthenticateModel> ValidateRefresh(string token)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            var login = await _cacheManager.GetCache(token);
            var refresh = new RefreshToken(token);
            if (!string.IsNullOrEmpty(login))
            {
                var user = await _userRepository.GetByLogin(login);
                await _cacheManager.Remove(token);
                if (refresh.Expiration >= (DateTime.UtcNow.ToUniversalTime() - origin).TotalSeconds)
                {
                    //TODO: вернуть токен
                    return CreateToken(user);
                }
            }

            return null;
        }

        public string AuthorizationCode(string clientId)
        {
            if (clientId != _oauthConfig.ClientId)
                return null;

            var code = Base64Helper.Base64Encode(Guid.NewGuid().ToString()).Substring(0, 10);
            _cacheManager.SetCache(code, "authcode");
            return $"/oauth?code={code}";
        }

        private string CreateRefresh(User user = null)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            var refToken = new RefreshToken()
            {
                Value = Guid.NewGuid().ToString(),
                Expiration = (int)(DateTime.UtcNow.Add(TimeSpan.FromSeconds(_oauthConfig.LifeSpan)).ToUniversalTime() - origin).TotalSeconds
            };
            var refreshToken = Base64Helper.Base64Encode(refToken.ToJson());

            _cacheManager.SetCache(refreshToken, user?.Login ?? "<app>");
            return refreshToken;
        }

        public async Task<AuthenticateModel> CreateToken(string code, string clientSecret, string clientId)
        {
            var value = await _cacheManager.GetCache(code);
            if (value == "authcode")
            {
                await _cacheManager.Remove(code);
                return CreateToken(null);
            }
            return null;
        }

        public AuthenticateModel CreateToken(User user)
        {
            var refreshToken = CreateRefresh(user);
            var accessToken = _jwtAuth.Create(user);

            return new AuthenticateModel
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
    }
}
