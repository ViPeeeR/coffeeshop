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
        private readonly ILogger<JwtAuth> _logger;
        private readonly JwtSecurityConfig _config;

        public JwtAuth(IOptions<JwtSecurityConfig> options,
            ILogger<JwtAuth> logger)
        {
            _logger = logger;
            _config = options.Value;
        }

        public string Create(User user)
        {
            var expirationTime = DateTime.UtcNow.AddSeconds(_config.LifeSpan);

            Claim[] claims = null;

            if (user != null)
            {
                claims = new[]
               {
                new Claim(ClaimTypes.Name, user?.Login),
                new Claim(ClaimTypes.Role, user?.Role.ToString()),
                };
            }

            var jwt = new JwtSecurityToken(
                issuer: _config.Issuer,
                audience: _config.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.Add(TimeSpan.FromSeconds(_config.LifeSpan)),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.SigningKey)),
                    SecurityAlgorithms.HmacSha256
                ));

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwt);

            return accessToken;
        }

        public bool Validate(string token)
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
    }
}
