using CoffeeShops.Common;
using CoffeeShops.Session.API.Abstracts;
using CoffeeShops.Session.API.Infrustucture;
using CoffeeShops.Session.API.Infrustucture.Helpers;
using CoffeeShops.Session.API.Infrustucture.Redis;
using CoffeeShops.Session.API.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShops.Session.API.Infrastructure
{
    public class JwtAuth : IJwtAuth
    {
        private readonly ICacheManager _cacheManager;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<JwtAuth> _logger;
        private readonly OAuthConfig _oauthConfig;
        private readonly JwtSecurityConfig _config;

        public JwtAuth(IOptions<JwtSecurityConfig> options,
            ICacheManager cacheManager,
            IUserRepository userRepository,
            ILogger<JwtAuth> logger,
            IOptions<OAuthConfig> optionsOauth)
        {
            _cacheManager = cacheManager;
            _userRepository = userRepository;
            _logger = logger;
            _oauthConfig = optionsOauth.Value;
            _config = options.Value;
        }

        public AuthenticateModel CreateToken(User user)
        {
            var expirationTime = DateTime.UtcNow.AddSeconds(_config.LifeSpan);

            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.Name, user?.Login),
                new Claim(ClaimTypes.Role, user?.Role.ToString()),
            };

            var jwt = new JwtSecurityToken(
                issuer: _config.Issuer,
                audience: _config.Audience,
                claims: user != null ? claims : null,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.Add(TimeSpan.FromSeconds(_config.LifeSpan)),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.SigningKey)),
                    SecurityAlgorithms.HmacSha256
                ));

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwt);
            var refToken = new RefreshToken() { Value = Guid.NewGuid().ToString(), Expiration = DateTime.UtcNow.Add(TimeSpan.FromSeconds(_config.LifeSpan)).Second };
            var refreshToken = Base64Helper.Base64Encode(refToken.ToJson());

            _cacheManager.SetCache(refreshToken, user?.Login ?? "<empty>");

            return new AuthenticateModel
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public string AuthorizationCode(string clientId)
        {
            if (clientId != _oauthConfig.ClientId)
                return null;

            var code = Base64Helper.Base64Encode(Guid.NewGuid().ToString()).Substring(0, 10);
            _cacheManager.SetCache(code, "authcode");
            return code;
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

        public bool ValidateAccess(string token)
        {
            SecurityToken securityToken = null;
            var options = new TokenValidationParameters()
            {
                ValidIssuer = _config.Issuer,
                ValidAudience = _config.Audience,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.SigningKey)),
            };

            try
            {
                var claims = new JwtSecurityTokenHandler().ValidateToken(token, options, out securityToken);
            }
            catch (SecurityTokenException)
            {
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }

            return securityToken != null;
        }

        public async Task<AuthenticateModel> ValidateRefresh(string token)
        {
            var login = await _cacheManager.GetCache(token);
            var refresh = new RefreshToken(token);
            if (!string.IsNullOrEmpty(login))
            {
                var user = await _userRepository.GetByLogin(login);
                await _cacheManager.Remove(token);
                if (refresh.Expiration >= DateTime.UtcNow.Second)
                    return CreateToken(user);
            }

            return null;
        }
    }
}
